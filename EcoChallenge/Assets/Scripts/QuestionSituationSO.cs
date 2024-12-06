using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionSituationSO", menuName = "ScriptableObjects/QuestionSituationSO", order = 2)]
public class QuestionSituationSO : ScriptableObject
{
    public List<QuestionSituation> Questions;
}
