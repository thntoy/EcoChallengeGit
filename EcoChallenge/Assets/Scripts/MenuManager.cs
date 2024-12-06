using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public enum MenuState
    {
        Main,
        Purpose,
        Authentication,
        Instructions,
        Character,
        Lobby
    }

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _instructionsPanel;
    [SerializeField] private GameObject _purposePanel;
    [SerializeField] private GameObject _authenticationPanel;
    [SerializeField] private GameObject _characterPanel;
    [SerializeField] private GameObject _lobbyPanel;

    [SerializeField] private Button _loginButton;
    [SerializeField] private TMP_InputField _usernameInputField;

    [SerializeField] private TextMeshProUGUI[] _instructionsTexts;
    [SerializeField] private TextMeshProUGUI[] _purposeTexts;

    private int _currentInstructionsIndex;
    private int _currentPurposeIndex;

    public MenuState menuState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeMenuState(MenuState.Main);
    }

    private void Update()
    {
        if (_usernameInputField.text.Length > 10)
        {
            _usernameInputField.text = _usernameInputField.text.Substring(0, 20);
        }

        if (_usernameInputField.text.Length >= 3 && _usernameInputField.text.Length <= 20)
        {
            _loginButton.interactable = true;
        }
        else
        {
            _loginButton.interactable = false;
        }

    }

    public void ChangeMenuState(MenuState state)
    {
        menuState = state;
        _mainPanel.SetActive(false);
        _instructionsPanel.SetActive(false);
        _purposePanel.SetActive(false);
        _authenticationPanel.SetActive(false);
        _characterPanel.SetActive(false);
        _lobbyPanel.SetActive(false);


        switch (state)
        {
            case MenuState.Main:
                _mainPanel.SetActive(true);
                break;
            case MenuState.Instructions:
                _instructionsPanel.SetActive(true);
                break;
            case MenuState.Purpose:
                _purposePanel.SetActive(true);
                break;
            case MenuState.Authentication:
                _authenticationPanel.SetActive(true);
                break;
            case MenuState.Character:
                _characterPanel.SetActive(true);
                break;
            case MenuState.Lobby:
                _lobbyPanel.SetActive(true);
                break;
        }
    }

    public void ChangeMenuState(int state)
    {
        ChangeMenuState((MenuState)state);
    }

    private IEnumerator ChangeMenuStateAfterDelayCoroutine(MenuState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeMenuState(state);
    }

    public void ChangeMenuStateAfterDelay(MenuState state, float delay)
    {
        StartCoroutine(ChangeMenuStateAfterDelayCoroutine(state, delay));
    }

    public void NextInstructionsText()
    {
        _instructionsTexts[_currentInstructionsIndex].gameObject.SetActive(false);
        _currentInstructionsIndex++;
        if (_currentInstructionsIndex >= _instructionsTexts.Length)
        {
            _currentInstructionsIndex = 0;
        }
        _instructionsTexts[_currentInstructionsIndex].gameObject.SetActive(true);
    }

    public void PreviousInstructionsText()
    {
        _instructionsTexts[_currentInstructionsIndex].gameObject.SetActive(false);
        _currentInstructionsIndex--;
        if (_currentInstructionsIndex < 0)
        {
            _currentInstructionsIndex = _instructionsTexts.Length - 1;
        }
        _instructionsTexts[_currentInstructionsIndex].gameObject.SetActive(true);
    }

    public void NextPurposeText()
    {
        _purposeTexts[_currentPurposeIndex].gameObject.SetActive(false);
        _currentPurposeIndex++;
        if (_currentPurposeIndex >= _purposeTexts.Length)
        {
            _currentPurposeIndex = 0;
        }
        _purposeTexts[_currentPurposeIndex].gameObject.SetActive(true);
    }

    public void PreviousPurposeText()
    {
        _purposeTexts[_currentPurposeIndex].gameObject.SetActive(false);
        _currentPurposeIndex--;
        if (_currentPurposeIndex < 0)
        {
            _currentPurposeIndex = _purposeTexts.Length - 1;
        }
        _purposeTexts[_currentPurposeIndex].gameObject.SetActive(true);
    }
}