using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    int WhoSecond;
    int WhoMinute;
    Coroutine currentPlayerAnswerCoroutine;
    Coroutine currentGameTimeCoroutine;
    public static TimeManager instance { get; private set; }

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
        
    }
    public void StartGameTimeOptions()
    {
        WhoMinute = GameManager.instance.GetGameTime();
        WhoSecond = 59;
        WhoMinute--;
        SetGameTimeCoroutine();
    }
    // Her Player1 Rope Atiliminda, X 0.75 Artacak.
    // Her Player1 Player Atiliminda, X 0.6 Artacak.
    public IEnumerator Player1TimerPositionControl(PlayerBehavior _player, GameObject _rope)
    {
        Vector2 _targetPlayerPosition = new Vector2(_player.gameObject.transform.localPosition.x + 0.6f, _player.gameObject.transform.localPosition.y);
        Vector2 _targetRopePosition = new Vector2(_rope.transform.localPosition.x + 0.75f, _rope.transform.localPosition.y);

        float duration = 2.0f; // Hareket süresi (daha küçük bir deðer kullanarak hýzý artýrýn)
        float elapsed = 0.0f;

        Vector2 startPosPlayer = _player.gameObject.transform.localPosition;
        Vector2 startPosRope = _rope.transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration); // Zamaný 0-1 aralýðýna sýkýþtýr
             Vector2 _v = Vector2.Lerp(startPosPlayer, _targetPlayerPosition, t);
            _player.gameObject.transform.localPosition = new Vector3(_v.x, _v.y, -1);
            _rope.gameObject.transform.localPosition = Vector2.Lerp(startPosRope, _targetRopePosition, t);

            yield return new WaitForEndOfFrame();
        }
        if (_player.gameObject.transform.position.x >= 0.57f)
        {
            PlayerManager.instance.LosePlayer(_player);
        }
    }
    public IEnumerator Player2TimerPositionControl(PlayerBehavior _player, GameObject _rope)
    {
        Vector2 _targetPlayerPosition = new Vector2(_player.gameObject.transform.localPosition.x - 0.76f, _player.gameObject.transform.localPosition.y);
        Vector2 _targetRopePosition = new Vector2(_rope.transform.localPosition.x - 0.75f, _rope.transform.localPosition.y);

        float duration = 2.0f; // Hareket süresi (daha küçük bir deðer kullanarak hýzý artýrýn)
        float elapsed = 0.0f;

        Vector2 startPosPlayer = _player.gameObject.transform.localPosition;
        Vector2 startPosRope = _rope.transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration); // Zamaný 0-1 aralýðýna sýkýþtýr
            Vector2 _v = Vector2.Lerp(startPosPlayer, _targetPlayerPosition, t);
            _player.gameObject.transform.localPosition = new Vector3(_v.x, _v.y, -1);
            _rope.gameObject.transform.localPosition = Vector2.Lerp(startPosRope, _targetRopePosition, t);

            yield return new WaitForEndOfFrame();
        }
        if (_player.gameObject.transform.position.x <= 0.72f)
        {
            PlayerManager.instance.LosePlayer(_player);
        }
    }
    IEnumerator GameTimeControl()
    {
        // Onayliyor musun paneli acildigi zaman, oyun suresi durmali ve cevap suresi devam etmeli.
        while (WhoSecond >= 0)
        {
            UIManager.instance.SetGameTimeText(WhoMinute, WhoSecond);
            WhoSecond--;           
            if (WhoSecond <= 0)
            {
                if (WhoMinute <= 0)
                {
                    // Sure(Oyun) bitti.
                    GameManager.instance.EndTheGame(GameEndingType.TimeEnding);
                    break;
                }
                WhoMinute--;
                WhoSecond = 59;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void SetGameTimeCoroutine()
    {
        currentGameTimeCoroutine = StartCoroutine(GameTimeControl());
    }
    public void StopGameTimeCoroutine()
    {
        StopCoroutine(currentGameTimeCoroutine);
    }
    
    public IEnumerator PlayerAnswerTimeControl(int _second)
    {
        Debug.Log("Verilen sure => " + _second);
        int currentSecondTime = _second;
        AnswerClockController.instance.SetTime(_second);      
        int BirdActivetedWaitingCount = QuestionManager.instance.CurrentQuestion.AnswerSecondTime;
        if (BirdActivetedWaitingCount <= 15 && BirdActivetedWaitingCount >= 10)
        {
            BirdActivetedWaitingCount = 5;
        }
        else
        {
            BirdActivetedWaitingCount = 8;
        }
        while (currentSecondTime >= 0)
        {
            UIManager.instance.SetPlayerAnswerTimeText(currentSecondTime);
            AnswerClockController.instance.SetAndIncreaseFilledImage(currentSecondTime);
            if (currentSecondTime == BirdActivetedWaitingCount)
            {
                BirdManager.instance.BirdActivated(true);
            }
            if (currentSecondTime <= 0)
            {
                BirdManager.instance.BirdActivated(false);
                QuestionPanelController.instance.ResponseTimeOver();
                break;
            }
            currentSecondTime--;
            yield return new WaitForSeconds(1f);
        }
    }
    public void SetPlayerAnswerCoroutine(IEnumerator _c)
    {
        currentPlayerAnswerCoroutine = StartCoroutine(_c);
    }
    public void StopPlayerAnswerCoroutine()
    {
        StopCoroutine(currentPlayerAnswerCoroutine);
    }
}
