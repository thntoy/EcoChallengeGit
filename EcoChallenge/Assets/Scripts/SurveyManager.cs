using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurveyManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _surveyInputField;
    public string SurveyAnswer;

    public void SaveSurveyAnswer()
    {
        SurveyAnswer = _surveyInputField.text;
        PlayfabManager.Instance.CurrentPlayerData.SurveyAnswer = SurveyAnswer;
        PlayfabManager.Instance.SaveData();

    }
}
