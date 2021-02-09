using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private Button retryBuuton = default;
    [SerializeField] private Button titleButton = default;
    [SerializeField] private Button selectButton = default;
    [SerializeField] private Button soundButton = default;

    [SerializeField] private PauseView _pauseView = default;

    // Start is called before the first frame update
    void Start()
    {
        //pause
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ =>
            {
                _pauseView.PauseButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //リトライボタン
        retryBuuton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.RetryButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //タイトルに戻るボタン
        titleButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.TitleButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //選択画面に戻るボタン
        selectButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.SelectButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //音量設定画面表示
        soundButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                //_pauseView.RetryButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });
    }
}
