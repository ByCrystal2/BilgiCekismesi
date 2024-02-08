using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public abstract class Player
{    
    public int ID;
    public string Name;
    public PlayerType Type;
    public int Score;
    public Player Rival;
    private bool isMyTurn;
    public Player(int _id, string _name, PlayerType _type)
    {
        ID = _id;
        Name = _name;
        Type = _type;       
    }
    public void AddRivalPlayer(Player _rival)
    {
        Rival = _rival;
    }

    public void PullTheRope()
    {        
        // Bu methodda ip cekilecek.
        // Ex: Rival.Score -= 10;
    }
    public bool IsMyTurn()
    {
        return isMyTurn;
    }
    public void SetMyTurn(bool _turn)
    {
        isMyTurn = _turn;
    }
    public void SetRivalTurn()
    {
        Rival.SetMyTurn(!isMyTurn);
    }
    public void AddScore(int _score)
    {
        Score += _score;
        UIManager.instance.UpdatePlayerScoreTexts();
    }
    public void IncreaseScore(int _score)
    {
        if (Score - _score <= 0)
        {
            Score = 0;
            UIManager.instance.UpdatePlayerScoreTexts();
            return;
        }
        Score -= _score;
        UIManager.instance.UpdatePlayerScoreTexts();
    }
}
public enum PlayerType
{
    Player,
    AI
}
[System.Serializable]
public class AI : Player
{
    // Yapay zeka zorluguna gore islemler belirlenecek.
    DifficultyType AILevel;
    private int LowPercentageLimit;
    private int MediumPercentageLimit;
    private int HighPercentageLimit;

    private int PercentageQuestionDodging;
    private int QuestiongReadingTime;

    bool isAnsweredCorrectly;
    public AI(int _id, string _name, PlayerType _type, DifficultyType _aILevel) : base(_id, _name, _type)
    {
        AILevel = _aILevel;
        DifficultyOptions();
        SetQuestionReadingTime();
    }
    public void DifficultyOptions()
    {
        PercentageLimitControl();
    }
    public void PercentageQuestionDodgingControl(int _percentage)
    {
        PercentageQuestionDodging = _percentage;
    }
    public void SetQuestionReadingTime()
    {
        QuestiongReadingTime = Random.Range(5, 10);
    }    
    public void PercentageLimitControl()
    {
        switch (AILevel)
        {
            case DifficultyType.None:
                break;
            case DifficultyType.Low:
                LowPercentageLimit = 50;
                MediumPercentageLimit = 30;
                HighPercentageLimit = 10;

                PercentageQuestionDodgingControl(30);
                break;
            case DifficultyType.Medium:
                LowPercentageLimit = 70;
                MediumPercentageLimit = 50;
                HighPercentageLimit = 20;

                PercentageQuestionDodgingControl(20);
                break;
            case DifficultyType.High:
                LowPercentageLimit = 90;
                MediumPercentageLimit = 60;
                HighPercentageLimit = 50;

                PercentageQuestionDodgingControl(10);
                break;
            default:
                break;
        }
    }
    private void DodgingControl(Question _currentQuestion)
    {
        if (IsAnsweredDodging())
        {
            // dodge durumuda olacak kodlar...
            Debug.Log("AI Soruyu Es Gecti.");
            UIManager.instance.AIPassQuestionObjActivation(true);
        }
        else
        {
            // dodge olmama durumunda kodlar...
            Debug.Log("AI Soruyu Es Gecmedi.");
            AIAnswer(_currentQuestion);
        }
    }
    private bool IsAnsweredDodging()
    {
        int percentage = Random.Range(0, 100);
        Debug.Log("percentage => " + percentage + "/" + PercentageQuestionDodging);
        if (percentage <= PercentageQuestionDodging)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void AIAnswer(Question _question)
    {
        Debug.Log(_question.QuestionMessage);
        switch (_question.Type)
        {
            case QuestionType.None:
                break;
            case QuestionType.Easy:
                int percentage = Random.Range(0, 100);
                if (percentage <= LowPercentageLimit)
                {
                    Debug.Log("AI, Türü => " + _question.Type + " Olan soruyu dogru cevaplayacak. Yüzdelik Deðer => " + percentage + "/" + LowPercentageLimit); 
                    isAnsweredCorrectly = true;
                }
                else
                {
                    Debug.Log("AI, Türü => " + _question.Type + " Olan soruyu yanlýþ cevaplayacak. Yüzdelik Deðer => " + percentage + "/" + LowPercentageLimit);
                    isAnsweredCorrectly = false;
                }
                break;
            case QuestionType.Medium:
                int percentage1 = Random.Range(0, 100);
                if (percentage1 <= MediumPercentageLimit)
                {
                    Debug.Log("AI, Türü => " + _question.Type + " Olan soruyu dogru cevaplayacak. Yüzdelik Deðer => " + percentage1 + "/" + MediumPercentageLimit);
                    isAnsweredCorrectly = true;
                }
                else
                {
                    Debug.Log("AI, Türü => " + _question.Type + " Olan soruyu yanlýþ cevaplayacak. Yüzdelik Deðer => " + percentage1 + "/" + MediumPercentageLimit);
                    isAnsweredCorrectly = false;
                }
                break;
            case QuestionType.Hard:
                int percentage2 = Random.Range(0, 100);
                if (percentage2 <= HighPercentageLimit)
                {
                    Debug.Log("AI, Türü => " + _question.Type + " Olan soruyu dogru cevaplayacak. Yüzdelik Deðer => " + percentage2 + "/" + HighPercentageLimit);
                    isAnsweredCorrectly = true;
                }
                else
                {
                    Debug.Log("AI, Türü => " + _question.Type + " Olan soruyu yanlýþ cevaplayacak. Yüzdelik Deðer => " + percentage2 + "/" + HighPercentageLimit);
                    isAnsweredCorrectly = false;
                }
                break;
            default:
                break;
        }
        AnsweredCorrectlyControl(isAnsweredCorrectly);
    }
    private void AnsweredCorrectlyControl(bool _isCorrectly)
    {
        Question currentQuestion = QuestionManager.instance.CurrentQuestion;
        PlayerBehavior currentBehavior = PlayerManager.instance.playerBehaviors.Where(x => x.ID == ID).SingleOrDefault();
        BirdManager.instance.BirdActivated(false);
        if (_isCorrectly)
        {
            AddScore(currentQuestion._Score);            
            currentBehavior.RopePulling();
            currentBehavior.correctlySoundBehavior.PlayMyAudio();

            QuestionManager.instance.StopSetAnswerTimeCoroutine();
            QuestionManager.instance.SetCurrentQuestionBeRandom();
            PlayerManager.instance.NextPlayer();
        }
        else
        {
            currentBehavior.wrongSoundBeheavior.PlayMyAudio();
            IncreaseScore(currentQuestion._Score - (int)(currentQuestion._Score * ((int)currentQuestion.Type * 0.1f)));
            QuestionManager.instance.StopSetAnswerTimeCoroutine();
            QuestionManager.instance.SetCurrentQuestionBeRandom();
            PlayerManager.instance.NextPlayer();
        }
    }
    public IEnumerator WaitingForQuestionAnsweredTime(Question _currentQuestion)
    {
        int currentSecond = QuestiongReadingTime;
        while (currentSecond >= 0)
        {
            yield return new WaitForSeconds(1f);
            currentSecond--;
        }
        DodgingControl(_currentQuestion);
    }
}
[System.Serializable]
public class Gamer : Player
{
    public Gamer(int _id, string _name, PlayerType _type) : base(_id, _name, _type)
    {

    }
}

