using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _instructionsPanel;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _userPanel;
    [SerializeField] private GameObject _scorePanel;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _resultScoreText;

    [SerializeField] private TextMeshProUGUI _congratulationText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _timerImage;

    [SerializeField] private TextMeshProUGUI[] _resultScoreByLevelTexts;

    [SerializeField] private TextMeshProUGUI _userNameText;
    [SerializeField] private Image[] _userImages;

    private Sprite _userSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadUserSprite();
        UpdateUserPanel();
        UpdateScoreText(ScoreManager.Instance.Score);

    }

    public void LoadUserSprite()
    {
        string gender = CharacterSelector.Instance.SelectedGender.ToString();
        int characterIndex = CharacterSelector.Instance.CharacterIndex;

        // Load all sprites from the sprite sheet based on the selected gender
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Characters/" + gender);

        if (sprites != null && sprites.Length > characterIndex)
        {
            _userSprite = sprites[characterIndex];
        }
        else
        {
            Debug.LogError("Sprite not found: Check if characterIndex is correct or if sprites are correctly named.");
        }
    }


    public void UpdateUserPanel()
    {
        _userNameText.text = PlayfabManager.Instance.CurrentPlayerData.Name;
        foreach (Image userImage in _userImages)
        {
            userImage.sprite = _userSprite;
        }
    }

    public void UpdateCongratulationText(string text)
    {
        _congratulationText.text = text;
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
        _resultScoreText.text = score.ToString();
    }

    public void UpdateResultScoreText()
    {
        if(_resultScoreByLevelTexts.Length < 3)
        {
            return;
        }
        _resultScoreByLevelTexts[0].text = ScoreManager.Instance.ScoreLevel1.ToString();
        _resultScoreByLevelTexts[1].text = ScoreManager.Instance.ScoreLevel2.ToString();
        _resultScoreByLevelTexts[2].text = ScoreManager.Instance.ScoreLevel3.ToString();
    }

    public void UpdateTimerText(float time)
    {
        if(_timerText != null)
        {
            _timerText.text = time.ToString("F0");
            _timerImage.fillAmount = time / GameManager.Instance.GameDurations;
        }
    }

    public void ShowInstructionsPanel(bool show)
    {
        _instructionsPanel.SetActive(show);
    }

    public void ShowResultPanel(bool show)
    {
        _resultPanel.SetActive(show);
    }

    public void ShowUserPanel(bool show)
    {
        _userPanel.SetActive(show);
    }

    public void ShowScorePanel(bool show)
    {
        _scorePanel.SetActive(show);
    }

    public void DisableAllPanels()
    {
        _instructionsPanel.SetActive(false);
        _resultPanel.SetActive(false);
        _userPanel.SetActive(false);
        _scorePanel.SetActive(false);
    }

}
