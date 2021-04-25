using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Game;

public class PauseView : MonoBehaviour
{    
    [SerializeField] private GameObject pauseMenu = default;    //pauseメニュー
    [SerializeField] private Image  backBlackImage = default;    //薄暗い背景
    [SerializeField] private float menuFadePos = 300;      //メニューの消える位置
    [SerializeField] private float menuMoveSpeed = 0.5f;   //メニューの動く速度
    [SerializeField] private MenuButtonView _menuButtonView = default;
    [SerializeField] private SoundSliderView _soundSliderView = default;

    private float menuSetPos;//メニューの設置位置

    void Awake()
    {
        menuSetPos = pauseMenu.transform.localPosition.x; //メニューを出す位置
        pauseMenu.transform.DOLocalMoveX(menuSetPos - menuFadePos, 0.0f); //最初に見えなくしておく
        pauseMenu.SetActive(true);//オフになっていた時用
    }

    //止める
    public void PauseButton()
    {
        pauseMenu.transform.DOLocalMoveX(menuSetPos, menuMoveSpeed).SetEase(Ease.OutBounce);//横から出す
        backBlackImage.gameObject.SetActive(true);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //再開
    public void RestartButton()
    {
        pauseMenu.transform.DOLocalMoveX(menuSetPos - menuFadePos, menuMoveSpeed).SetEase(Ease.InOutBack);//横に戻す
        backBlackImage.gameObject.SetActive(false);
        _menuButtonView.NoButton();//確認画面出てたら閉じる
        _soundSliderView.CloseButton();//サウンドバーのイメージ表示されてたら閉じる
    }


}
