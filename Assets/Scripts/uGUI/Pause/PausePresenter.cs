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
    [SerializeField] private Button gameEndButoon = default;
    [SerializeField] private PauseView _pauseView = default;

    // Start is called before the first frame update
    void Start()
    {
        //pause
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Pause ||
                        GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Subscribe(_ => _pauseView.PauseButton());

        //リトライボタン
        retryBuuton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.SceneButton(GameState.Retry, "リトライしますか?"));//どうするか悩み中)

        //タイトルに戻るボタン
        titleButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.SceneButton(GameState.Title, "タイトルに戻りますか?"));

        //選択画面に戻るボタン
        selectButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.SceneButton(GameState.Start, "選択画面に戻りますか?"));

        //音量設定画面表示
        soundButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.SoundButton());

        //はい
        yesButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.YesButton());

        //いいえ
        noButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.NoButton());

        //サウンドの閉じるボタン
        closeButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.CloseButton());

        //ゲーム終了画面表示
        gameEndButoon.OnClickAsObservable()
            .Subscribe(_ => _pauseView.SceneButton(GameState.GameEnd, "ゲーム終了しますか?"));
    }
}
