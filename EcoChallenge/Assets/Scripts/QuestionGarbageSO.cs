using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionGarbageSO", menuName = "ScriptableObjects/QuestionGarbageSO", order = 1)]
public class QuestionGarbageSO : ScriptableObject
{
    public List<QuestionGarbage> Questions;
}
