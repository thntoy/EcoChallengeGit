using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionSituation
{
    [TextArea] public string QuestionText; 
    public List<string> Answers;           
    public int CorrectAnswerIndex;       
}

