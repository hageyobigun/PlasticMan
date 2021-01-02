using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultView : MonoBehaviour
{
    [SerializeField] private GameObject vsResultImage = null;
    [SerializeField] private GameObject rushWinResultImage = null;
    [SerializeField] private GameObject rushLoseResultImage = null;
    [SerializeField] private GameObject rushClearImage = null;

    [SerializeField] TextMeshProUGUI winText = null;
    [SerializeField] TextMeshProUGUI loseText = null;

    //bossrushで勝利
    public void RushGameWin()
    {
        rushWinResultImage.SetActive(true);
    }

    //bossrushで勝利
    public void RushGameClear()
    {
        SoundManager.Instance.StopBgm();//意味ないかも
        rushClearImage.SetActive(true);
    }

    //bossrushで敗北
    public void RushGameLose()
    {
        loseText.gameObject.SetActive(true);
        rushLoseResultImage.SetActive(true);
    }

    //VSで勝利
    public void VsGameWin()
    {
        winText.gameObject.SetActive(true);
        vsResultImage.SetActive(true);
    }

    //VSで敗北
    public void VsGameLose()
    {
        loseText.gameObject.SetActive(true);
        vsResultImage.SetActive(true);
    }
}
