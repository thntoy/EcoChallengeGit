using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int Score;

    public int ScoreLevel1;
    public int ScoreLevel2;
    public int ScoreLevel3;

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
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoadManager.Instance.DontDestroyOnLoadObjects.Add(gameObject);
    }

    public void UpdateScoreData(PlayerData playerData)
    {
        Score = playerData.Score;
        
    }

    public void IncreaseScore(int score)
    {
        Score += score;
        UIManager.Instance.UpdateScoreText(Score);
        PlayfabManager.Instance.CurrentPlayerData.Score = Score;

    }

    public void DecreaseScore(int score)
    {
        Score -= score;
        UIManager.Instance.UpdateScoreText(Score);
    }

    public void IncreaseScoreLevel1(int score)
    {
        ScoreLevel1 += score;
    }

    public void DecreaseScoreLevel1(int score)
    {
        ScoreLevel1 -= score;
    }

    public void IncreaseScoreLevel2(int score)
    {
        ScoreLevel2 += score;
    }

    public void DecreaseScoreLevel2(int score)
    {
        ScoreLevel2 -= score;
    }

    public void IncreaseScoreLevel3(int score)
    {
        ScoreLevel3 += score;
    }

    public void DecreaseScoreLevel3(int score)
    {
        ScoreLevel3 -= score;
    }


    public void SaveScore()
    {
        PlayfabManager.Instance.SaveData();
        Debug.Log("Score saved: " + Score);
    }

}
