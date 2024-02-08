using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UserInputPanelController : MonoBehaviour
{
    [SerializeField] TMP_InputField TxtBoxUser1;
    [SerializeField] TMP_InputField TxtBoxUser2;
    void OnEnable()
    {
        TxtBoxUser1.text = "";
        TxtBoxUser2.text = "";
        if (GameManager.instance.GetGameMod() == GameMode.SinglePlayer)
        {            
            TxtBoxUser2.gameObject.SetActive(false);
            TxtBoxUser1.gameObject.SetActive(true);
        }
        else
        {
            TxtBoxUser2.gameObject.SetActive(true);
            TxtBoxUser1.gameObject.SetActive(true);
        }
    }
    public void TextBox1ValueEndEdit()
    {
        PlayerManager.instance.PLayer1Name = TxtBoxUser1.text;
    }

    public void TextBox2ValueChanged() 
    {
        PlayerManager.instance.PLayer2Name = TxtBoxUser2.text;
    }
    public void StartTheGame()
    {
        UIManager.instance.StartGame();
    }
    public void CloseUserInputPanel()
    {
        UIManager.instance.UserPanelActivation(false);
        TxtBoxUser1.text = "";
        TxtBoxUser2.text = "";
    }
}
