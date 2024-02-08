using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopePullingController : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject rope;

    private Vector2 RopeStartPos;
    private GameObject winningPlayer;
    public PlayerBehavior currentBehavior;
    public static RopePullingController instance { get; private set; }
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
        RopeStartPos = rope.transform.position;
    }

    public void SetWinningPlayer(GameObject _player)
    {
        winningPlayer = _player;
        PlayerBehavior behavior = _player.GetComponent<PlayerBehavior>();
        currentBehavior = PlayerManager.instance.playerBehaviors.Where(x=> x.ID != behavior.ID).SingleOrDefault();
        if (currentBehavior.ID == GameManager.instance.GetPlayers()[0].ID)
        {
            StartCoroutine(TimeManager.instance.Player1TimerPositionControl(currentBehavior, rope));
        }
        else if (currentBehavior.ID == GameManager.instance.GetPlayers()[1].ID)
        {
            StartCoroutine(TimeManager.instance.Player2TimerPositionControl(currentBehavior, rope));
        }
    }
    public void ResetRopePos()
    {
        rope.transform.position = RopeStartPos;
    }
}
