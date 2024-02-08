using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DifficultyType CurrentGameDiff;
    private DifficultyType CurrentAIDif;
    private GameMode CurrentGameMode;
    private GameEndingType CurrentEndingType;
    private int GameTime;
    [SerializeField] List<Player> Players = new List<Player>();

    public float slowMotionScale = 0.1f; // Yavaþlatma oraný
    public float timeToSlowDown = 0.5f; // Yavaþlatmanýn tamamlanacaðý süre

    public static GameManager instance {  get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);        
    }
    private void Start()
    {
        AudioManager.instance.SetAllAudioSources();
    }
    public void EndTheGame(GameEndingType _type)
    {
        SetGameEndingType(_type);
        QuestionManager.instance.GameQuestions.Clear();
        TimeManager.instance.StopGameTimeCoroutine();
        TimeManager.instance.StopPlayerAnswerCoroutine();
        QuestionPanelController.instance.QuestionCount = 0;
        UIManager.instance.QuestionEndTextActivation(false);
        switch (CurrentEndingType)
        {
            case GameEndingType.None:
                break;
            case GameEndingType.TimeEnding:
                break;
            case GameEndingType.QuestionEnding:
                UIManager.instance.QuestionEndTextActivation(true);
                break;
            case GameEndingType.FallingIntoPlayerEnding:
                StartCoroutine(FantasyPauseGame());
                break;
            default:
                break;
        }

        UIManager.instance.EndGamePanelActivation(true);
        // Devam edilecek.
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetGameDifficulty(DifficultyType _difficulty){CurrentGameDiff = _difficulty;}/**/public DifficultyType GetGameDiff() { return CurrentGameDiff; }
    public void SetAIDifficulty(DifficultyType _difficulty){CurrentAIDif = _difficulty;}/**/public DifficultyType GetAIDiff() { return CurrentAIDif;}
    public void SetGameTime(int _time){GameTime = _time;}/**/public int GetGameTime() { return GameTime; }
    public void SetGameMode(GameMode _mode){CurrentGameMode = _mode;}/**/public GameMode GetGameMod() { return CurrentGameMode; }
    public void SetGameEndingType(GameEndingType _type){CurrentEndingType = _type;}/**/public GameEndingType GetGameEndingType() { return CurrentEndingType; }    
    
    public void AddPlayer(Player _newPlayer)
    {
        Players.Add(_newPlayer);
    }
    public List<Player> GetPlayers()
    {
        return Players;
    }
    public IEnumerator FantasyPauseGame()
    {
        float currentTime = 0f;

        while (currentTime < timeToSlowDown)
        {
            currentTime += Time.unscaledDeltaTime; // Gerçek zamaný kullanarak geçen süreyi hesapla
            float t = Mathf.Clamp01(currentTime / timeToSlowDown); // Yavaþlatma oranýný zamanla arttýr
            Time.timeScale = Mathf.Lerp(1f, slowMotionScale, t); // Yavaþlatmayý ayarla
            yield return null; // Bir sonraki frame'e geç
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
public enum GameEndingType
{
    None,
    TimeEnding,
    QuestionEnding,
    FallingIntoPlayerEnding
}
