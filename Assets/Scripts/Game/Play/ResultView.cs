using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultView : MonoBehaviour
{
    [SerializeField] private GameObject vsResultImage = null;
    [SerializeField] private GameObject rushWinResultImage = null;
    [SerializeField] private GameObject rushLoseResultImage = null;

    [SerializeField] TextMeshProUGUI winText = null;
    [SerializeField] TextMeshProUGUI loseText = null;

    //bossrushで勝利
    public void RushGameWin()
    {
        winText.enabled = true;
        rushWinResultImage.SetActive(true);
    }

    //bossrushで敗北
    public void RushGameLose()
    {
        loseText.enabled = true;
        rushLoseResultImage.SetActive(true);
    }

    //VSで勝利
    public void VsGameWin()
    {
        winText.enabled = true;
        vsResultImage.SetActive(true);
    }

    //VSで敗北
    public void VsGameLose()
    {
        loseText.enabled = true;
        vsResultImage.SetActive(true);
    }
}
