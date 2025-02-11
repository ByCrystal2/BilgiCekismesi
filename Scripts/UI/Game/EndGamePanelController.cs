using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EndGamePanelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI WinnerPlayerText;
    void OnEnable()
    {
        if (GameManager.instance.GetGameEndingType() == GameEndingType.FallingIntoPlayerEnding)
        {
            Player loserPlayer = PlayerManager.instance.LoserPlayer;
            WinnerPlayerText.text = loserPlayer.Rival.Name;
            return;
        }
        Player highestScorer = GameManager.instance.GetPlayers().OrderBy(x => x.Score).Last();
        WinnerPlayerText.text = highestScorer.Name;
    }
}
