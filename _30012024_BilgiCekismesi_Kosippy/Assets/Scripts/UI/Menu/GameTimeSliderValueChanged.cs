using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeSliderValueChanged : MonoBehaviour
{
    [SerializeField] Slider GameTimeSlider;
    [SerializeField] Text GameTimeText;

    [SerializeField] float testing;
    public static GameTimeSliderValueChanged instance {  get; private set; }
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
        //GameTimeText.text = "1 DK";
        GameTimeSliderValueChenged();
    }
    public void GameTimeSliderValueChenged() // Game Time Slider / OnValueChanged Event
    {
        GameTimeText.text = GameTimeSlider.value + " DK";
        Debug.Log(GameTimeText.text);
    }
    public int GetGameTimeSliderValue()
    {
        return (int)GameTimeSlider.value;
    }
}
