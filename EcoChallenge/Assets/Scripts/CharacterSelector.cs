using System.Collections;
using System.Collections.Generic;
using PlayFab.SharedModels;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector Instance;
    public Gender SelectedGender;
    public int CharacterIndex;

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

    public void UpdateCharacterData(PlayerData playerData)
    {
        SelectedGender = playerData.Gender;
        CharacterIndex = playerData.CharacterIndex;
    }

    public void SelectCharacter(int index)
    {
        CharacterIndex = index;
        PlayfabManager.Instance.CurrentPlayerData.CharacterIndex = CharacterIndex;
    }

    public void SelectGender(int genderIndex)
    {
        SelectedGender = (Gender)genderIndex;   
        PlayfabManager.Instance.CurrentPlayerData.Gender = SelectedGender;
    }
}
public enum Gender
{
    Male,
    Female
}
