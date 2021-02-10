using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Game;

public class PauseView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI backText = default; //確認画面のテキスト
    [SerializeField] private GameObject pauseMenu = default;    //pauseメニュー
    [SerializeField] private Image backBlackImage = default;    //薄暗い背景
    [SerializeField] private GameObject confirmImage = default; //確認画面
    [SerializeField] private GameObject SoundBar = default; //音量調整


    private float menuPos;       //メニューの位置
    private bool isPause;        //pauseかどうか
    private GameState moveScene; //どこのシーンに行くか

    void Awake()
    {
        menuPos = pauseMenu.transform.localPosition.x;
        isPause = false;
        moveScene = GameState.Play;
        pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.0f);
        backBlackImage.gameObject.SetActive(false);
    }

    public void PauseButton()
    {
        if (!isPause)
        {
            pauseMenu.transform.DOLocalMoveX(menuPos, 0.5f).SetEase(Ease.OutBounce);
            backBlackImage.gameObject.SetActive(true);
            isPause = true;
        }
        else
        {
            pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.5f).SetEase(Ease.InOutBack);
            backBlackImage.gameObject.SetActive(false);
            isPause = false;
        }
    }

    public void RetryButton(GameState gameState)
    {
        moveScene = gameState;
        backText.text = "やり直しますか?";
        confirmImage.SetActive(true);
    }

    public void TitleButton(GameState gameState)
    {
        moveScene = gameState;
        backText.text = "タイトルに戻りますか?";
        confirmImage.SetActive(true);
    }

    public void SelectButton(GameState gameState)
    {
        moveScene = gameState;
        backText.text = "選択画面に戻りますか?";
        confirmImage.SetActive(true);
    }

    //あとで
    public void SoundButton()
    {
        SoundBar.SetActive(true);
    }

    //シーン移動
    public void YesButton()
    {
        GameManeger.Instance.currentGameStates.Value = moveScene;
    }

    //シーン移動しない
    public void NoButton()
    {
        confirmImage.SetActive(false);
    }

}
