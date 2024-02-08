using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class AnswerClockController : MonoBehaviour 
{
    [SerializeField] Image FilledImage;
    int _currentSecond;
    public static AnswerClockController instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void SetAndIncreaseFilledImage(int _second)
    {
        FilledImage.fillAmount = Mathf.Clamp01((float)_second / _currentSecond);
    }
    public void SetTime(int _time)
    {
        _currentSecond = _time;
    }
}
