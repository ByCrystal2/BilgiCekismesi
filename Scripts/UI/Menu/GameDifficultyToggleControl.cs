using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDifficultyToggleControl : MonoBehaviour
{
    [SerializeField]  Toggle ToggleLow;
    [SerializeField]  Toggle ToggleLMedium;
    [SerializeField]  Toggle ToggleHigh;
    public static GameDifficultyToggleControl instance {  get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public DifficultyType GetAndControlToggleValue() // Start Butonuna Baglanacak.
    {
        if (ToggleLow.isOn)
        {
            return DifficultyType.Low;
        }
        else if (ToggleLMedium.isOn)
        {
            return DifficultyType.Medium;
        }
        else if (ToggleHigh.isOn)
        {
            return DifficultyType.High;
        }
        else
        {
            return DifficultyType.None;
        }
    }
}

