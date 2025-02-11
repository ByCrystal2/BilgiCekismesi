using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer
{
    public int ID;
    public string AnswerMessage;
    public bool isSelected;
    public bool isTrue;
    public AnswerType AnswerType;
    public Answer(int _id, string _answerMessage, AnswerType answerType, bool _isTrue = false, bool _isSelected = false)
    {
        ID = _id;
        AnswerMessage = _answerMessage;
        isTrue = _isTrue;
        isSelected = _isSelected;
        AnswerType = answerType;
    }
}
public enum AnswerType
{
    None,
    A,
    B,
    C,
    D
}
