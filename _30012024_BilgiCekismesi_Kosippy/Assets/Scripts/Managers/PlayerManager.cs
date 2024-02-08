using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player CurrentPlayer;
    public Player LoserPlayer;
    public List<PlayerBehavior> playerBehaviors;
    public string PLayer1Name;
    public string PLayer2Name;
    public static PlayerManager instance { get; private set; }
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
    public void AnswerPriority()
    {
        List<Player> playerList = GameManager.instance.GetPlayers();
        int random = Random.Range(0, playerList.Count);
        Player p = playerList[random];
        CurrentPlayer = p;
        if (p.Type == PlayerType.AI)
        {
            QuestionPanelController.instance.AllToggleActivation(false);
            if (p is AI _ai)
            {
                Debug.Log("AI is Priority => " + _ai.Name);
                Debug.Log("CurrentQuestion => " + QuestionManager.instance.CurrentQuestion.QuestionMessage);
                StartCoroutine(_ai.WaitingForQuestionAnsweredTime(QuestionManager.instance.CurrentQuestion));
            }
        }
        else
        {
            QuestionPanelController.instance.AllToggleActivation(true);
        }
        RopePullingController.instance.currentBehavior = playerBehaviors.Where(x=> x.ID == p.ID).SingleOrDefault();
        p.SetMyTurn(true);
        p.SetRivalTurn();
        
    }
    public void CreatePlayers(GameMode _gameMode)
    {
        playerBehaviors.Clear();

        string p1Name = PLayer1Name;
        if (PLayer1Name == "")
        {
            p1Name = "User1";
        }
        Debug.Log("Player 1 Name => " + p1Name);
        Gamer player1 = new Gamer(0, p1Name, PlayerType.Player);
        GameManager.instance.AddPlayer(player1);
        if (_gameMode == GameMode.SinglePlayer)
        {
            AI ai = new AI(1, AINames[Random.Range(0, AINames.Count)], PlayerType.AI,GameManager.instance.GetAIDiff());
            PLayer2Name = ai.Name;
            ai.AddRivalPlayer(player1);
            ai.Rival.AddRivalPlayer(ai);            
            GameManager.instance.AddPlayer(ai);         
        }
        else
        {
            string p2Name = PLayer2Name;
            if (PLayer2Name == "")
            {
                p2Name = "User1";
            }
            Gamer player2 = new Gamer(2, p2Name, PlayerType.Player);
            player2.AddRivalPlayer(player1);
            player2.Rival.AddRivalPlayer(player2);
            GameManager.instance.AddPlayer(player2);
        }

    }
    public void NextPlayer()
    {
        Debug.Log(CurrentPlayer.Name);
        Player nextPlayer = GameManager.instance.GetPlayers().Where(x => x.ID != CurrentPlayer.ID).FirstOrDefault();
        nextPlayer.SetMyTurn(true); 
        nextPlayer.SetRivalTurn();
        if (nextPlayer.Type == PlayerType.AI)
        {
            if (nextPlayer is AI _ai)
            {
                QuestionPanelController.instance.AllToggleActivation(false);
                UIManager.instance.PlayersHandActivationControl(true, false);
                StartCoroutine(_ai.WaitingForQuestionAnsweredTime(QuestionManager.instance.CurrentQuestion));
            }
        }
        else // PlayerType.Player
        {
            if (nextPlayer.ID == GameManager.instance.GetPlayers()[0].ID)
            {
                UIManager.instance.PlayersHandActivationControl(true, true);
            }
            else
            {
                UIManager.instance.PlayersHandActivationControl(true, false);
            }
            QuestionPanelController.instance.AllToggleActivation(true);
        }
        CurrentPlayer = nextPlayer;
    }
    public Player GetPlayerOfTurn()
    {
        return GameManager.instance.GetPlayers().Where(x=> x.IsMyTurn()).SingleOrDefault();
    }
    public List<PlayerBehavior> GetPlayerBehaviors()
    {
        return playerBehaviors;
    }
    public int SetPlayer1Score(Player _player)
    {
        return _player.Score;
    }
    public void RestartPlayerOptions()
    {
        List<Player> players = GameManager.instance.GetPlayers();
        foreach (Player _player in players)
        {
            _player.Score = 0;
        }
        foreach (var player in playerBehaviors)
        {
            player.ResertPos();
        }
        UIManager.instance.UpdatePlayerScoreTexts();
    }
    public void LosePlayer(PlayerBehavior _LoserPlayer)
    {
        GameManager.instance.EndTheGame(GameEndingType.FallingIntoPlayerEnding);
    }
    public List<string> AINames = new List<string>() { "Alex Johnson", "Maria Rodriguez", "Maria Rodriguez", "Emily Davis" };
}
