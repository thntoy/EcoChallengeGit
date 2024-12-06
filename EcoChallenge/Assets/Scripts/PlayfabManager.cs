using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager Instance;
    public PlayerData CurrentPlayerData;

    [SerializeField] private CharacterSpriteLoader _characterSpriteLoader;

    public string UserName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (transform.parent != null)
                transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DontDestroyOnLoadManager.Instance.DontDestroyOnLoadObjects.Add(gameObject);
    }

    public void Login(TMP_InputField usernameInput)
    {
        PlayFab.PlayFabClientAPI.LoginWithCustomID(new PlayFab.ClientModels.LoginWithCustomIDRequest
        {
            CustomId = usernameInput.text,
            CreateAccount = true
        }, OnLoginSuccess, OnLoginFailure);

        UserName = usernameInput.text;
    }

    public void Logout()
    {
        PlayFab.PlayFabClientAPI.ForgetAllCredentials();
    }

    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {
        Debug.Log("Successfully logged in");
        SaveUserName(UserName); 
        UpdateDisplayName(UserName);       
        GetPlayerData();

        MenuManager.Instance.ChangeMenuStateAfterDelay(MenuManager.MenuState.Lobby, 1.5f);
    }

    private void OnLoginFailure(PlayFab.PlayFabError error)
    {
        Debug.LogWarning("Failed to log in: " + error.GenerateErrorReport());
    }

    public void GetPlayerData()
    {
        PlayFab.PlayFabClientAPI.GetUserData(new PlayFab.ClientModels.GetUserDataRequest(), OnGetPlayerDataSuccess, OnGetPlayerDataFailure);
    }
    
    private void OnGetPlayerDataSuccess(PlayFab.ClientModels.GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Name") || result.Data.ContainsKey("Gender") || result.Data.ContainsKey("CharacterIndex") || result.Data.ContainsKey("Score") || result.Data.ContainsKey("SurveyAnswer"))
        {
            CurrentPlayerData.Name = result.Data["Name"].Value;
            CurrentPlayerData.Gender = (Gender)System.Enum.Parse(typeof(Gender), result.Data["Gender"].Value);
            CurrentPlayerData.CharacterIndex = int.Parse(result.Data["CharacterIndex"].Value);
            CurrentPlayerData.Score = int.Parse(result.Data["Score"].Value);
            CurrentPlayerData.SurveyAnswer = result.Data["SurveyAnswer"].Value;

            CharacterSelector.Instance.UpdateCharacterData(CurrentPlayerData);

            if (_characterSpriteLoader != null)
            {
                _characterSpriteLoader.LoadUserSprite();
            }

            Debug.Log("Successfully loaded player data");
        }
        else
        {
            Debug.Log("Data not found");
        }
    }

    private void OnGetPlayerDataFailure(PlayFab.PlayFabError error)
    {
        Debug.LogWarning("Failed to get player data: " + error.GenerateErrorReport());
    }

    public void SaveData()
    {
        PlayFab.PlayFabClientAPI.UpdateUserData(new PlayFab.ClientModels.UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Name", CurrentPlayerData.Name},
                {"Gender", CurrentPlayerData.Gender.ToString()},
                {"CharacterIndex", CurrentPlayerData.CharacterIndex.ToString()},
                {"Score", CurrentPlayerData.Score.ToString()},
                {"SurveyAnswer", CurrentPlayerData.SurveyAnswer}
            }
        }, result =>
        {
            Debug.Log("Successfully saved data");
            UpdateDisplayName(CurrentPlayerData.Name); // Update DisplayName after saving data
        }, OnSaveDataFailure);
    }

    private void SaveUserName(string name)
    {
        CurrentPlayerData.Name = name;
    }

    private void OnSaveDataFailure(PlayFab.PlayFabError error)
    {
        Debug.LogWarning("Failed to save data: " + error.GenerateErrorReport());
    }

    public void UpdateDisplayName(string displayName)
    {
        PlayFab.PlayFabClientAPI.UpdateUserTitleDisplayName(new PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        }, OnUpdateDisplayNameSuccess, OnUpdateDisplayNameFailure);
    }

    private void OnUpdateDisplayNameSuccess(PlayFab.ClientModels.UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Successfully updated DisplayName: " + result.DisplayName);
    }

    private void OnUpdateDisplayNameFailure(PlayFab.PlayFabError error)
    {
        Debug.LogWarning("Failed to update DisplayName: " + error.GenerateErrorReport());
    }
}
