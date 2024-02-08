using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {  get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;        
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            AudioManager.instance.SetAllAudioSources();
            AudioManager.instance.PlayGameClip();
            List<Player> currentPlayers = GameManager.instance.GetPlayers();
            PlayerManager.instance.playerBehaviors = FindObjectsOfType<PlayerBehavior>().ToList();
            List<PlayerBehavior> playerBehaviors = PlayerManager.instance.playerBehaviors;
            playerBehaviors[0].SetPLayer(currentPlayers[0]);
            playerBehaviors[1].SetPLayer(currentPlayers[1]);
            SetPlayer1ScoreText(currentPlayers[0].Score);
            SetPlayer2ScoreText(currentPlayers[1].Score);
            SetPlayer1NameText(currentPlayers[0].Name);
            SetPlayer2NameText(currentPlayers[1].Name);
            QuestionManager.instance.CreateQuestions();
            QuestionManager.instance.SetCurrentQuestionBeRandom();
            PlayerManager.instance.AnswerPriority();
            TimeManager.instance.StartGameTimeOptions();
        }
    }

    [Header("Menu Scene")]
    [SerializeField] GameObject UserInputPanel;
    #region Menu
    private void SendToGameMode()
    {
        GameManager.instance.SetGameMode(GameModeDdValueChanged.GetAndControlGameModeDdValue());
    }
    private void SendToGameDiff()
    {
        GameManager.instance.SetGameDifficulty(GameDifficultyToggleControl.instance.GetAndControlToggleValue());
    }
    private void SendToGameTime()
    {
        GameManager.instance.SetGameTime(GameTimeSliderValueChanged.instance.GetGameTimeSliderValue());
    }
    private void SendToAIDiff()
    {
        if (AIDifficultySliderValueChanged.instance != null)           
            GameManager.instance.SetAIDifficulty(AIDifficultySliderValueChanged.instance.GetAndControlAIDiffSliderValue());
        else
            GameManager.instance.SetAIDifficulty(DifficultyType.None);
    }
    public void StartGame()
    {
        SendToGameMode();
        SendToGameDiff();
        SendToGameTime();
        SendToAIDiff();

        PlayerManager.instance.CreatePlayers(GameManager.instance.GetGameMod());
        SceneManager.LoadScene("Game");
        
        // Burdan devam edecegiz...
    }
    public void UserPanelActivation(bool _active)
    {
        UserInputPanel.SetActive(_active);        
    }
    #endregion
    [Header("Game Scene")]
    #region Game
    [SerializeField] TextMeshProUGUI GameTimeText;
    [SerializeField] TextMeshProUGUI Player1ScoreText;
    [SerializeField] TextMeshProUGUI Player2ScoreText;
    [SerializeField] TextMeshProUGUI Player1NameText;
    [SerializeField] TextMeshProUGUI Player2NameText;

    [SerializeField] TextMeshProUGUI PlayerAnswerTimeText;
    [SerializeField] GameObject QuestionEndText;

    [SerializeField] GameObject EndGamePanel;
    [SerializeField] GameObject OptionsPanel;

    [SerializeField] GameObject Player1Hand;
    [SerializeField] GameObject Player2Hand;
    [SerializeField] GameObject AIPassQuestionObj; 

    public void UpdatePlayerScoreTexts()
    {
        List<Player> currentPlayers = new List<Player>();
        currentPlayers = GameManager.instance.GetPlayers();
        SetPlayer1ScoreText(currentPlayers[0].Score);
        SetPlayer2ScoreText(currentPlayers[1].Score);        
    }
    public void SetPlayer1ScoreText(int _score)
    {
        int currentValue = int.Parse(Player1ScoreText.text);
        DOTween.To(() => currentValue, x => currentValue = x, _score, 4f)
            .SetEase(Ease.OutQuad)
            .OnUpdate(() => {
                // Text'i güncelle
                Player1ScoreText.text = "" + _score;
            });        
    }
    public void SetPlayer2ScoreText(int _score)
    {
        int currentValue = int.Parse(Player2ScoreText.text);
        DOTween.To(() => currentValue, x => currentValue = x, _score, 4f)
            .SetEase(Ease.OutQuad)
            .OnUpdate(() => {
                // Text'i güncelle
                Player2ScoreText.text = "" + _score;
            });
    }
    public void SetPlayer1NameText(string _name)
    {
        Player1NameText.text = "" + _name;
    }
    public void SetPlayer2NameText(string _name)
    {
        Player2NameText.text = "" + _name;
    }
    public void QuestionEndTextActivation(bool _active)
    {
        QuestionEndText.SetActive(_active);
    }
    public void OptionsPanelActivation(bool _active)
    {
        OptionsPanel.SetActive(_active);
    }
    public void AIPassQuestionObjActivation(bool _active)
    {
        AIPassQuestionObj.SetActive(_active);
    }
    public void PlayersHandActivationControl(bool _active, bool _isPlayer1)
    {
        Player1Hand.SetActive(!_active);
        Player2Hand.SetActive(!_active);
        if (_isPlayer1)
            Player1Hand.SetActive(_active);
        else
            Player2Hand.SetActive(_active);
    }
    public void SetGameTimeText(int _minute, int _second)
    {
        if (_second < 10)
            GameTimeText.text = _minute + ":0" + _second;
        else
            GameTimeText.text = _minute + ":" +_second;
    }
    public void SetPlayerAnswerTimeText(int _second)
    {
        if (PlayerAnswerTimeText!=null)
        {
            if (_second < 10)
                PlayerAnswerTimeText.text = "0" + _second + "sc.";
            else
                PlayerAnswerTimeText.text = "" + _second + "sc.";

        }
    }
    
    public void RestartGame() // EndGame Restart Button on click
    {
        GameManager.instance.ResumeGame();
        QuestionManager.instance.CreateQuestions();
        TimeManager.instance.StartGameTimeOptions();
        PlayerManager.instance.RestartPlayerOptions();
        RopePullingController.instance.ResetRopePos();
        QuestionManager.instance.SetCurrentQuestionBeRandom();
        PlayerManager.instance.AnswerPriority();
        // devam edilecek...
        EndGamePanelActivation(false);
    }
    public void ExitTheGame()
    {
        GameManager.instance.ExitGame();
    }
    public void GoMenu()// pnlEndGame / GoMenuButton
    {
        PlayerManager.instance.RestartPlayerOptions();
        SceneManager.LoadScene("Menu");
    }
    public void EndGamePanelActivation(bool _active) // pnlEndGame / RestartGameButton
    {
        EndGamePanel.SetActive(_active);
    }
    #endregion
}
public enum DifficultyType
{
    None,
    Low,
    Medium,
    High
}
public enum GameMode
{
    None,
    SinglePlayer,
    CouplePlayer
}