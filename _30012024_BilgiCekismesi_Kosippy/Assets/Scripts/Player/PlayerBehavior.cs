using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int ID;
    private Player player;
    [SerializeField] public Vector3 StartPos;
    public SoundBehavior correctlySoundBehavior;
    public SoundBehavior wrongSoundBeheavior;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        correctlySoundBehavior = GetComponent<SoundBehavior>();
        wrongSoundBeheavior = transform.GetChild(0).GetComponent<SoundBehavior>();
        StartPos = transform.position;
    }
    public void ResertPos()
    {
        transform.position = StartPos;
    }
    public void SetPLayer(Player player)
    {
        this.player = player;
        ID = player.ID;
    }
    public bool IsMyPlayerTurm()
    {
        return player.IsMyTurn();
    }
    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Idle") && stateInfo.normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            animator.SetTrigger("Idle");
        }
    }
    public void RopePulling()
    {
        animator.ResetTrigger("Idle");
        animator.SetTrigger("Pulling");
        RopePullingController.instance.SetWinningPlayer(gameObject);
    }
}
