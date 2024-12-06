using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SituationQuestionController : MonoBehaviour
{
    public static SituationQuestionController Instance;

    [SerializeField] private GameObject _questionPanel;
    [SerializeField] private GameObject _answerPanel;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Button[] _answerButtons;
    [SerializeField] private Button _stopButton;
    [SerializeField] private QuestionSituationSO questionSO;

    [SerializeField] private AudioClip _correctSFX;
    [SerializeField] private AudioClip _wrongSFX;

    private List<int> _randomizedQuestions = new List<int>(); // Holds randomized question indices
    private int _questionCount = 5; // Number of questions player needs to answer
    private Coroutine _randomQuestionCoroutine;
    private int _currentQuestionIndex = 0;
    private int _displayedQuestionIndex = -1; // Tracks the last displayed question for answer validation
    private int _questionsAnswered = 0; // Counter for the number of questions answered
    private List<int> _unusedQuestions = new List<int>();

    private bool _isStopped = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _stopButton.onClick.AddListener(StopRandomQuestion);
    }

    public void StartRandomQuestion()
{
    _isStopped = false; // Reset the stop flag
    _questionPanel.SetActive(true);
    _answerPanel.SetActive(false);

    // Prepare a randomized list of question indices
    PrepareRandomizedQuestions();

    // Start the coroutine to show questions randomly over time
    if (_randomQuestionCoroutine == null)
    {
        _randomQuestionCoroutine = StartCoroutine(ShowRandomQuestionOverTime());
    }
}


private void PrepareRandomizedQuestions()
{
    // If we've used all questions, reset the list
    if (_unusedQuestions.Count == 0)
    {
        // Create a new list excluding already displayed questions
        var allQuestions = Enumerable.Range(0, questionSO.Questions.Count).ToList();
        var remainingQuestions = allQuestions.Except(_randomizedQuestions).ToList();

        // If no remaining questions are left, reset everything
        if (remainingQuestions.Count == 0)
        {
            _randomizedQuestions.Clear();
            remainingQuestions = allQuestions;
        }

        // Shuffle the remaining questions
        _unusedQuestions = remainingQuestions.OrderBy(x => Random.value).ToList();
        Debug.Log("Unused questions: " + string.Join(", ", _unusedQuestions));
    }
}

private IEnumerator ShowRandomQuestionOverTime()
{
    while (!_isStopped) // Loop until the stop button is clicked
    {
        if (_unusedQuestions.Count == 0) // If no questions are left, reshuffle
        {
            PrepareRandomizedQuestions();
        }

        // Select the next question from the list
        _displayedQuestionIndex = _unusedQuestions[0];
        _unusedQuestions.RemoveAt(0); // Remove the question from the unused list

        ShowQuestion(_displayedQuestionIndex);

        yield return new WaitForSeconds(0.2f); // Adjust the interval as needed
    }

    Debug.Log("Random question stopped by player.");
}



    public void ShowQuestion(int questionIndex)
    {
        _questionText.text = questionSO.Questions[questionIndex].QuestionText;
    }

   public void StopRandomQuestion()
{
    _isStopped = true; // Indicate that the stop button was clicked
    _answerPanel.SetActive(true);
    _stopButton.interactable = false;

    if (_randomQuestionCoroutine != null)
    {
        StopCoroutine(_randomQuestionCoroutine);
        _randomQuestionCoroutine = null;
    }

    Debug.Log("Question index: " + _displayedQuestionIndex + " Answer index: " + questionSO.Questions[_displayedQuestionIndex].CorrectAnswerIndex);

    // Set the answer text on each button
    for (int i = 0; i < _answerButtons.Length; i++)
    {
        int index = i;
        _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questionSO.Questions[_displayedQuestionIndex].Answers[i];
        _answerButtons[i].onClick.RemoveAllListeners();
        _answerButtons[i].interactable = true;

        _answerButtons[i].onClick.AddListener(() =>
        {
            HandleAnswer(index);
            _answerButtons[index].interactable = false;
        });
    }

    _randomizedQuestions.Add(_displayedQuestionIndex); // Add to displayed list

    Debug.Log("Stopped random question");
}

    private void HandleAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == questionSO.Questions[_displayedQuestionIndex].CorrectAnswerIndex)
        {
            ScoreManager.Instance.IncreaseScore(2);
            ScoreManager.Instance.IncreaseScoreLevel3(2);
            PopupHandler.Instance.SpawnCorrectPopup(_answerButtons[selectedAnswerIndex].transform.position);
            AudioManager.Instance.PlayEffect(_correctSFX);
            Debug.Log("Correct answer");
        }
        else
        {
            ScoreManager.Instance.DecreaseScore(2);
            ScoreManager.Instance.DecreaseScoreLevel3(2);
            PopupHandler.Instance.SpawnWrongPopup(_answerButtons[selectedAnswerIndex].transform.position);
            AudioManager.Instance.PlayEffect(_wrongSFX);
            Debug.Log("Wrong answer");
        }

        _questionsAnswered++;
        Debug.Log("Questions answered: " + _questionsAnswered);

        if (_questionsAnswered >= _questionCount)
        {
            // Call the result state after the fifth question is answered
            StartCoroutine(HandleGameStateWithDelay());
            Debug.Log("Transitioning to result state.");
        }
        else
        {
            // Start a new question if we haven't reached the limit
            StartCoroutine(StartNewQuestion());
        }
    }

    private IEnumerator StartNewQuestion()
    {
        yield return new WaitForSeconds(1f);

        _questionPanel.SetActive(true);
        _answerPanel.SetActive(false);
        _stopButton.interactable = true;
        StartRandomQuestion();
    }

    public void ResetQuestions()
    {
        _unusedQuestions.Clear(); // Clear the unused questions
        _currentQuestionIndex = 0;
        _questionsAnswered = 0;
    }


    private IEnumerator HandleGameStateWithDelay()
    {
        yield return new WaitForSeconds(1f);
        _questionPanel.SetActive(false);
        GameManager.Instance.SetGameState(GameState.Result);
    }
}
