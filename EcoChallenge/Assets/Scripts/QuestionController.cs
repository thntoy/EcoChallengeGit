using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GarbageQuestionController : MonoBehaviour
{
    public static GarbageQuestionController Instance;

    [SerializeField] private GameObject _answerPanel;
    [SerializeField] private Image _questionImage;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Button[] _answerButtons;
    [SerializeField] private QuestionGarbageSO questionSO;

    [SerializeField] private AudioClip _correctSFX;
    [SerializeField] private AudioClip _wrongSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowQuestion(int questionIndex)
    {
        _answerPanel.SetActive(true);

        _questionImage.sprite = Resources.Load<Sprite>("Sprites/Garbages/" + questionSO.Questions[questionIndex].ItemName);
        _questionText.text = questionSO.Questions[questionIndex].QuestionText;

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questionSO.Questions[questionIndex].Answers[i];
        }
    }

    public void ShowQuestion(string itemName)
    {
        _answerPanel.SetActive(true);

        QuestionGarbage question = questionSO.Questions.Find(q => q.ItemName == itemName);

        _questionImage.sprite = Resources.Load<Sprite>("Sprites/Garbages/" + question.ItemName);
        _questionText.text = question.QuestionText;

        // Clear all button listeners
        foreach (Button button in _answerButtons)
        {
            button.onClick.RemoveAllListeners();
            button.interactable = true;
        }

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            int index = i;
            _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.Answers[i];
            
            if (i == question.CorrectAnswerIndex)
            {
                // Add listener for the correct answer
                _answerButtons[i].onClick.AddListener(() =>
                {
                    ScoreManager.Instance.IncreaseScore(1);
                    ScoreManager.Instance.IncreaseScoreLevel2(1);
                    PopupHandler.Instance.SpawnCorrectPopup(_answerButtons[index].transform.position);
                    AudioManager.Instance.PlayEffect(_correctSFX);
                    foreach (Button button in _answerButtons)
                    {
                        button.interactable = false;
                    }
                    StartCoroutine(HandleAnswerPanelWithDelay());
                });
            }
            else
            {
                // Add listener for incorrect answers
                _answerButtons[i].onClick.AddListener(() =>
                {
                    ScoreManager.Instance.DecreaseScore(1);
                    ScoreManager.Instance.DecreaseScoreLevel2(1);
                    PopupHandler.Instance.SpawnWrongPopup(_answerButtons[index].transform.position);
                    AudioManager.Instance.PlayEffect(_wrongSFX);
                    
                    foreach (Button button in _answerButtons)
                    {
                        button.interactable = false;
                    }
                    StartCoroutine(HandleAnswerPanelWithDelay());
                });
            }
        }
    }

    private IEnumerator HandleAnswerPanelWithDelay()
    {
        // Wait for the delay to hide the answer panel
        yield return new WaitForSeconds(1f);
        _answerPanel.SetActive(false);

        TruckController.Instance.UnfreezeTruck();

        if (GarbageSpawner.Instance.IsNoGarbageLeft())
        {
            GameManager.Instance.SetGameState(GameState.Result);
        }
    }
}
