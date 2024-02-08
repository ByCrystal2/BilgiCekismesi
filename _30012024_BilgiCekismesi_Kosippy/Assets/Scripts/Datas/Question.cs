using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public int ID;
    public string QuestionMessage;
    public QuestionType Type;
    public int _Score;
    public int AnswerSecondTime;
    [SerializeField] List<Answer> MyAnswers = new List<Answer>();
    public Question(int _id, string _questionMessage, List<Answer> _answers, QuestionType _type, int _score)
    {
        ID = _id;
        QuestionMessage = _questionMessage;
        if (_answers.Count == 4)
        {
            List<Answer> _answerList = new List<Answer>();
            _answerList.Clear();
            int length = _answers.Count;
            for (int i = 0; i < length; i++)
            {
                _answerList.Add(_answers[i]);
            }
            MyAnswers = _answerList;
        }
        else
            Debug.Log("Lutfen en az/fazla 4 cevap tanimlayiniz.");
        Type = _type;
        _Score = _score + (_score * ((int)Type / 2));
        AnswerSecondTime = (int)(10 + (_Score * ((int)Type * 0.03f))); // Duzenlenicek... Hatali sistem.
        //Debug.Log("Score => " + _score + "Type =>" + Type +"AnswerSecondTime => " + AnswerSecondTime);
        // Dunyanýn en buyuk dagý, 70, medium => 14,2(14) saniye
    }
    public List<Answer> GetMyAnswers()
    {
        return MyAnswers;
    }
}
