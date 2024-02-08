using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelController : MonoBehaviour
{
    [SerializeField] GameObject pnlTrueFalse;
    [SerializeField] public TextMeshProUGUI QuestionHeaderText;
    [SerializeField] Text QuestionMessageText;
    [SerializeField] Toggle[] Toggles;
    [SerializeField] Toggle FlagToggle;
    [SerializeField] Text[] AnswerTexts;

    SoundBehavior SoundBehavior;
    public int QuestionCount = 0;
    public static QuestionPanelController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SoundBehavior = GetComponent<SoundBehavior>();
    }
    public void SetQuestionPanelUIS(Question _question)
    {
        QuestionMessageText.text = _question.QuestionMessage;
        List<Answer> answers = _question.GetMyAnswers();
        int length = AnswerTexts.Length;
        for (int i = 0; i < length; i++)
        {
            AnswerTexts[i].text = answers[i].AnswerMessage;
        }

        QuestionCount++;
        QuestionHeaderText.text = "Soru " + QuestionCount; 
    }
    public void PlayNextQuestionSoundEffect()
    {
        SoundBehavior.PlayMyAudio();
    }
    public void ActivationTrueFalsePanel(bool _activation)
    {
        pnlTrueFalse.SetActive(_activation);
    }
    public void AllToggleActivation(bool _active)
    {
        foreach (Toggle toggle in Toggles)
        {
            toggle.interactable = _active;
        }
    }
    public void UserChoiseAnswerClicked(bool _isTickSelection)// YesAndNoPanel / Yes and No Buttons / onClicked event
    {
        if (_isTickSelection)
        {
            AnswerCorrectnessCheck();
            TimeManager.instance.SetGameTimeCoroutine();
        }
        else
        {
            // X e tiklarsa olacaklar.
            TimeManager.instance.SetGameTimeCoroutine();
        }
    }
    public void AnswerCorrectnessCheck()
    {
        Question currentQuestion = QuestionManager.instance.CurrentQuestion;       
        Player currentPlayer = GameManager.instance.GetPlayers().Where(x=> x.IsMyTurn()).SingleOrDefault();
        PlayerBehavior currentBehavior = PlayerManager.instance.playerBehaviors.Where(x => x.ID == currentPlayer.ID).SingleOrDefault();

        Answer answer = currentQuestion.GetMyAnswers().Where(x => x.isSelected).SingleOrDefault();
        BirdManager.instance.BirdActivated(false);
        if (QuestionManager.instance.IsCurrentAnswerOfQuestionCorrect(answer))
        {
            currentPlayer.AddScore(currentQuestion._Score);
            
            currentBehavior.correctlySoundBehavior.PlayMyAudio();            
            currentBehavior.RopePulling();

            QuestionManager.instance.StopSetAnswerTimeCoroutine();
            QuestionManager.instance.SetCurrentQuestionBeRandom();
            PlayerManager.instance.NextPlayer();
        }
        else
        {
            // 150 - Hard
            currentBehavior.wrongSoundBeheavior.PlayMyAudio();
            AnswerWrong(currentPlayer, currentQuestion);
        }
    }
    public void AnswerWrong(Player _currentPlayer,Question _currentQuestion)//105
    {
        _currentPlayer.IncreaseScore(_currentQuestion._Score - (int)(_currentQuestion._Score * ((int)_currentQuestion.Type * 0.1f)));
        QuestionManager.instance.StopSetAnswerTimeCoroutine();
        QuestionManager.instance.SetCurrentQuestionBeRandom();
        PlayerManager.instance.NextPlayer();
    }
    public void ResponseTimeOver()//70
    {
        Debug.Log("ResponseTimeOver => " + "Entered.");
        Player _currentPlayer = GameManager.instance.GetPlayers().Where(x => x.IsMyTurn()).SingleOrDefault();
        Question _currentQuestion = QuestionManager.instance.CurrentQuestion;
        _currentPlayer.IncreaseScore((int)((_currentQuestion._Score - (int)(_currentQuestion._Score * ((int)_currentQuestion.Type * 0.1f))) / 1.5f));
        QuestionManager.instance.StopSetAnswerTimeCoroutine();
        if (pnlTrueFalse.activeInHierarchy)
        {
            TimeManager.instance.SetGameTimeCoroutine();
        }
        ActivationTrueFalsePanel(false);
        QuestionManager.instance.SetCurrentQuestionBeRandom();
        PlayerManager.instance.NextPlayer();
    }
    public void FlagToggleIsOn()
    {
        FlagToggle.isOn = true;
    }
}