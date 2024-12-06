using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float GameDurations = 60f;

    public static GameManager Instance;
    public GameState CurrentGameState;
    
    public static event Action OnGameAwake;
    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;
    public static event Action OnTimesUp;

    [SerializeField] private bool _enableTimer = true;
    

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

        OnGameAwake?.Invoke();
    }

    private void Start()
    {
        SetGameState(GameState.Instructions);
    }

    private void Update()
    {
        if(_enableTimer){
            if (CurrentGameState == GameState.Play)
            {
                GameDurations -= Time.deltaTime;
                UIManager.Instance.UpdateTimerText(GameDurations);
                if (GameDurations <= 0)
                {
                    SetGameState(GameState.Result);
                    OnTimesUp?.Invoke();
                }
            }
        }
        
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;

        switch (state)
        {
            case GameState.Instructions:
                UIManager.Instance.DisableAllPanels();
                UIManager.Instance.ShowInstructionsPanel(true);
                break;
            case GameState.Play:
                UIManager.Instance.DisableAllPanels();
                UIManager.Instance.ShowUserPanel(true);
                UIManager.Instance.ShowScorePanel(true);
                OnGameStart.Invoke();
                break;
            case GameState.Pause:
                UIManager.Instance.DisableAllPanels();
                break;
            case GameState.Result:
                UIManager.Instance.DisableAllPanels();
                UIManager.Instance.ShowResultPanel(true);
                OnGameEnd.Invoke();
                break;
            
        }
    }

    public void SetGameState(int state)
    {
        SetGameState((GameState)state);
    }

    public void GameEnd()
    {
        DontDestroyOnLoadManager.Instance.DestroyAllDontDestroyOnLoadObjects();
    }

    public void GameSaveScore()
    {
        ScoreManager.Instance.SaveScore();
    }

}
public enum GameState
{
    Instructions,
    Play,
    Pause,
    Result
}