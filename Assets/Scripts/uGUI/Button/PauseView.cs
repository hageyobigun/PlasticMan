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
        pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.0f);
        backBlackImage.gameObject.SetActive(false);
        SoundBar.SetActive(false);
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
            backBlackImage.gameObject.SetActive(false);
            SoundBar.SetActive(false);
        }
    }

    //あとで
    public void SoundButton()
    {
        confirmImage.SetActive(false);
        SoundBar.SetActive(true);
    }

    //シーン移動するボタン(リトライ、タイトル、選択画面)
    public void SceneButton(GameState gameState, string text)
    {
        backText.text = text;
        confirmImage.SetActive(true);
        SoundBar.SetActive(false);
        moveScene = gameState;
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

    public void CloseButton()
    {
        SoundBar.SetActive(false);
    }

}
