using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameModeDdValueChanged : MonoBehaviour
{
    [SerializeField] GameObject AIDiffObj;
    [SerializeField] Button StartButton;
    static TMP_Dropdown dropdown;
    static List<string> options = new List<string>();
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.captionText.text = "Oyun Modu Seçiniz.";
        StartButton.interactable = false;
    }
    private void Start()
    {
        options = new List<string>() { "Oyun Modunu Seçiniz.","Tek Kiþilik", "Çift Kiþilik"};        
        dropdown.AddOptions(options);
    }
    public static GameMode GetAndControlGameModeDdValue()
    {
        if (dropdown.value == 0)
            return GameMode.None;
        else if(dropdown.value == 1)
            return GameMode.SinglePlayer;
        else if (dropdown.value == 2)
            return GameMode.CouplePlayer;
        else
            return GameMode.None;
    }
    public void GameModeDropdownValueChanged()
    {
        if (dropdown.value == 0)
        {
            StartButton.interactable = false;
            AIDiffObj.SetActive(false);            
            return;
        }
        else if (dropdown.value == 1)
        {
            AIDiffObj.SetActive(true);
            GameManager.instance.SetGameMode(GameMode.SinglePlayer);

        }
        else if (dropdown.value == 2)
        {
            AIDiffObj.SetActive(false);
            GameManager.instance.SetGameMode(GameMode.CouplePlayer);
        }

        Debug.Log(dropdown.value);
        StartButton.interactable = true;
    }
}
