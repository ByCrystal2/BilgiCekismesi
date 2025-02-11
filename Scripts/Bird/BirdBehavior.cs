using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    [SerializeField] Vector2 StartPos;
    [SerializeField] Vector2 TargetPos;
    [SerializeField] BehaviourType CurrentType;
    [SerializeField] Sprite DefaultSprite;
    [SerializeField] Sprite CuckoSprite;
    [SerializeField] float ExitSpeed;
    [SerializeField] float EnterSpeed;
    public int WaitSecond;
    Transform BirdPos;
    SpriteRenderer BirdRenderer;
    SoundBehavior SoundBehavior;
   public void BaseOptions()
    {
        BirdPos = transform;
        BirdRenderer = GetComponent<SpriteRenderer>();        
        BirdPos.localPosition = StartPos;
    }
    private void Awake()
    {
        if (CurrentType == BehaviourType.Bird)
        {
            SoundBehavior = GetComponent<SoundBehavior>();
        }
    }
    private void OnEnable()
    {
        BaseOptions();
        if (CurrentType == BehaviourType.Bird)
        {
            SetMySprite(DefaultSprite);
        }        
        GoToTargetPos();
    }
    public void GoToTargetPos()
    {
        try
        {
            BirdPos.DOLocalMoveX(TargetPos.x, ExitSpeed).OnComplete(() => {
                if (CurrentType == BehaviourType.Bird)
                {
                    SetMySprite(CuckoSprite);
                    SoundBehavior.PlayMyAudio();
                    StartCoroutine(StartWaiting(WaitSecond));
                }
                else
                {
                    StartCoroutine(StartWaiting(WaitSecond));
                }
                Debug.Log("Hareket tamamlandý!");
                // Baþka iþlemler ekle...
            });
        }
        catch (System.Exception)
        {

            Debug.Log("Oyuncu sistem calisirken cevap verdi");
        }
        
        
    }
    private void GoToStartPos()
    {
        try
        {
            if (gameObject.activeInHierarchy)
            {
                BirdPos.DOLocalMoveX(StartPos.x, EnterSpeed).OnComplete(() => {
                    // Hareket tamamlandýktan sonra yapýlacak iþlemler burada olacak
                    Debug.Log("Hareket tamamlandý!");
                    // Baþka iþlemler ekle...
                });
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Oyuncu sistem calisirken cevap verdi");
        }
        
        
    }
    private IEnumerator StartWaiting(int _second)
    {
        int WaitSecond = _second;
        Debug.Log("Waiting Basladi.." + _second);
        while (WaitSecond >= 0)
        {
            WaitSecond--;
            yield return new WaitForSeconds(1f);
        }
        GoToStartPos();
        if (CurrentType == BehaviourType.Bird)
        {
            SetMySprite(DefaultSprite);
        }
    }
    public void SetMySprite(Sprite _sprite)
    {
        if (gameObject.activeInHierarchy)
        {
            BirdRenderer.sprite = _sprite;
        }
    }
    private void OnDisable()
    {
        BirdPos.localPosition = StartPos;
    }
    public enum BehaviourType
    {
        None,
        Bird,
        Stick
    }
}
