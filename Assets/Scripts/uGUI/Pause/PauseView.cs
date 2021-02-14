using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Game;

public class PauseView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI backText = default; //確認画面のテキスト
    [SerializeField] private GameObject pauseMenu = default;    //pauseメニュー
    [SerializeField] private Image  backBlackImage = default;    //薄暗い背景
    [SerializeField] private GameObject confirmImage = default; //確認画面
    [SerializeField] private GameObject soundBar = default; //音量調整

    private float menuPos;       //メニューの位置
    private GameState moveScene; //どこのシーンに行くか

    private SelectImageOpen _selectImageOpen;

    void Awake()
    {
        menuPos = pauseMenu.transform.localPosition.x; //メニューを出す位置
        //最初に全部見えなくしておく
        pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.0f);
        backBlackImage.gameObject.SetActive(false);
        soundBar.SetActive(false);
        _selectImageOpen = new SelectImageOpen();
    }

    public void PauseButton()
    {
        SoundManager.Instance.PlaySe("NormalButton");
        //止める
        if (!(GameManeger.Instance.currentGameStates.Value == GameState.Pause))
        {
            GameManeger.Instance.currentGameStates.Value = GameState.Pause;
            pauseMenu.transform.DOLocalMoveX(menuPos, 0.5f).SetEase(Ease.OutBounce);
            backBlackImage.gameObject.SetActive(true);
        }
        //動かす
        else
        {
            GameManeger.Instance.currentGameStates.Value = GameState.Play;
            pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.5f).SetEase(Ease.InOutBack);
            backBlackImage.gameObject.SetActive(false);
            soundBar.SetActive(false);
        }
    }
    //シーン移動するボタン(リトライ、タイトル、選択画面)
    public void SceneButton(GameState gameState, string text)
    {
        backText.text = text;
        confirmImage.SetActive(true);
        soundBar.SetActive(false);
        moveScene = gameState;
        SoundManager.Instance.PlaySe("NormalButton");
        _selectImageOpen.ImageOpen(confirmImage);
    }

    //あとで
    public void SoundButton()
    {
        confirmImage.SetActive(false);
        soundBar.SetActive(true);
        SoundManager.Instance.PlaySe("NormalButton");
        _selectImageOpen.ImageOpen(soundBar);
    }

    //シーン移動
    public void YesButton()
    {
        GameManeger.Instance.currentGameStates.Value = moveScene;
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //シーン移動しない
    public void NoButton()
    {
        confirmImage.SetActive(false);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //閉じるボタン(sound)
    public void CloseButton()
    {
        soundBar.SetActive(false);
        SoundManager.Instance.PlaySe("NormalButton");
    }

}
