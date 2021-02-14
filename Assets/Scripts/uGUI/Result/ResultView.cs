using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResultView : MonoBehaviour
{
    [SerializeField] private NextRushGame _nextRushGame = default;
    [SerializeField] private GameObject resultImage = default;
    [SerializeField] TextMeshProUGUI winText = default;
    [SerializeField] TextMeshProUGUI loseText = default;
    [SerializeField] TextMeshProUGUI clearText = default;



    //BossRushで勝利
    public void RushGameWin()
    {
        _nextRushGame.NextSequence();
    }

    //bossRushクリア
    public void RushGameClear()
    {
        clearText.gameObject.SetActive(true);
        resultImage.SetActive(true);
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
