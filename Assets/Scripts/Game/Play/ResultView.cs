using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResultView : MonoBehaviour
{
    [SerializeField] private GameObject rushWinResultImage = default;
    [SerializeField] private GameObject rushClearImage = default;
    [SerializeField] private GameObject resultImage = default;

    [SerializeField] TextMeshProUGUI winText = default;
    [SerializeField] TextMeshProUGUI loseText = default;

    //bossrushで勝利
    public void RushGameWin()
    {
        rushWinResultImage.SetActive(true);
    }

    //bossrushで勝利
    public void RushGameClear()
    {
        rushClearImage.SetActive(true);
    }

    //VSで勝利
    public void VsGameWin()
    {
        winText.gameObject.SetActive(true);
        resultImage.SetActive(true);
    }

    //敗北
    public void GameLose()
    {
        loseText.gameObject.SetActive(true);
        resultImage.SetActive(true);
    }
}
