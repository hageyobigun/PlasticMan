using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText = default;
    [SerializeField] TextMeshProUGUI loseText = default;
    [SerializeField] TextMeshProUGUI clearText = default;
    [SerializeField] TextMeshProUGUI nextText = default;

    [SerializeField] private GameObject resultMene = default;//リザルト画面
    [SerializeField] private Image backBlackImage = default;//背景の黒

    private ResultImageOpen _resultImageOpen; //リザルト画面を開く演出
    private NextGameView _nextGameView; //次の敵へ行く演出

    private void Start()
    {
        _resultImageOpen = new ResultImageOpen();
        _nextGameView = new NextGameView();
    }

    //BossRushで勝利
    public void RushGameWin() =>
        _nextGameView.NextStage(nextText, backBlackImage);//boss rushの次へ行く演出
    
    //bossRushクリア
    public void RushGameClear() => ResultOpen(clearText);
    
    //VSで勝利
    public void VsGameWin() => ResultOpen(winText);
    
    //敗北
    public void GameLose() =>  ResultOpen(loseText);

    //リザルト画面表示
    private void ResultOpen(TextMeshProUGUI resultText)
    {
        resultText.gameObject.SetActive(true);
        backBlackImage.gameObject.SetActive(true);
        resultMene.SetActive(true);
        _resultImageOpen.ResultOpen(resultMene);
    }
}
