using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Game;

public class PauseView : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI menuText = default; //確認画面のテキスト
    [SerializeField] private GameObject pauseMenu = default;    //pauseメニュー
    [SerializeField] private Image  backBlackImage = default;    //薄暗い背景
    [SerializeField] private GameObject confirmImage = default; //確認画面
    [SerializeField] private GameObject soundBarImage= default; //音量調整

    private float menuPos;       //メニューの位置
    private GameObject ActiveImage; //開いているイメージ
    private GameState moveScene; //どこのシーンに行くか
    private SelectImageOpen _selectImageOpen;

    void Awake()
    {
        ActiveImage = null;
        menuPos = pauseMenu.transform.localPosition.x; //メニューを出す位置
        //最初に見えなくしておく
        pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.0f);
        _selectImageOpen = new SelectImageOpen();
    }

    //止める
    public void PauseButton()
    {
        pauseMenu.transform.DOLocalMoveX(menuPos, 0.5f).SetEase(Ease.OutBounce);//横から出す
        backBlackImage.gameObject.SetActive(true);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //再開
    public void RestartButton()
    {
        pauseMenu.transform.DOLocalMoveX(menuPos - 300, 0.5f).SetEase(Ease.InOutBack);//横に戻す
        backBlackImage.gameObject.SetActive(false);
        ActiveImage.SetActive(false);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //シーン移動するボタン(リトライ、タイトル、選択画面)
    public void SceneButton(GameState gameState, string text)
    {
        menuText.text = text;
        moveScene = gameState;
        MenuImageOpen(confirmImage);
    }

    //音量調整出す
    public void SoundButton()
    {
        MenuImageOpen(soundBarImage);
    }

    //表示
    public void MenuImageOpen(GameObject selectImage)
    {
        selectImage.SetActive(true);
        _selectImageOpen.ImageOpen(selectImage);
        ActiveImage = selectImage;
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //シーン移動
    public void YesButton()
    {
        GameManeger.Instance.currentGameStates.Value = moveScene;
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //閉じるボタン(close, No)
    public void CloseButton()
    {
        if (ActiveImage.activeInHierarchy != false && ActiveImage != null)//何も開いてないときは動かない
        {
            ActiveImage.SetActive(false);
            SoundManager.Instance.PlaySe("NormalButton");
        }
    }

}
