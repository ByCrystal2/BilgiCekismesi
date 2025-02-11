using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIDifficultySliderValueChanged : MonoBehaviour
{
    [SerializeField] Slider AIDiffSlider;
    public static AIDifficultySliderValueChanged instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public DifficultyType GetAndControlAIDiffSliderValue()
    {        
        if (AIDiffSlider.value == 1)
            return DifficultyType.Low;
        else if (AIDiffSlider.value == 2)
            return DifficultyType.Medium;
        else if (AIDiffSlider.value == 3)
            return DifficultyType.High;
        else
            return DifficultyType.None;
    }
}