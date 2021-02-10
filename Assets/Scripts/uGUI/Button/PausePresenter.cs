using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Game;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private Button retryBuuton = default;
    [SerializeField] private Button titleButton = default;
    [SerializeField] private Button selectButton = default;
    [SerializeField] private Button soundButton = default;
    [SerializeField] private Button yesButton = default;
    [SerializeField] private Button noButton = default;
    [SerializeField] private Button closeButton = default;
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
                _pauseView.SceneButton(GameState.Play, "リトライしますか?");//どうするか悩み中
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //タイトルに戻るボタン
        titleButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.SceneButton(GameState.Title, "タイトルに戻りますか?");//どうするか悩み中
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //選択画面に戻るボタン
        selectButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.SceneButton(GameState.Start, "選択画面に戻りますか?");//どうするか悩み中
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //音量設定画面表示
        soundButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.SoundButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //はい
        yesButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.YesButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        //いいえ
        noButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.NoButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

        closeButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _pauseView.CloseButton();
                SoundManager.Instance.PlaySe("NormalButton");
            });

    }
}
