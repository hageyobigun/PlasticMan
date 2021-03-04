using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Game;
using UnityEngine.EventSystems;

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

    [SerializeField] private EventSystem eventSystem = default;

    private GameObject pushButton;//押したボタン


    // Start is called before the first frame update
    void Start()
    {
        pushButton = null;

        //pause
        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("pause"))
            .Subscribe(_ =>
            {
                //同時に行ってしまうので一緒にした
                if (GameManeger.Instance.currentGameStates.Value == GameState.Play)//pause
                {
                    eventSystem.SetSelectedGameObject(retryBuuton.gameObject);
                    _pauseView.PauseButton();
                    GameManeger.Instance.currentGameStates.Value = GameState.Pause;
                }
                else if(GameManeger.Instance.currentGameStates.Value == GameState.Pause)//再開
                {
                    eventSystem.SetSelectedGameObject(null);
                    _pauseView.RestartButton();
                    GameManeger.Instance.currentGameStates.Value = GameState.Play;
                }
            });

        //リトライボタン
        retryBuuton.OnClickAsObservable()
            .Subscribe(_ => SetMoveSceneButton(GameState.Retry, "リトライしますか?"));

        //タイトルに戻るボタン
        titleButton.OnClickAsObservable()
            .Subscribe(_ => SetMoveSceneButton(GameState.Title, "タイトルに戻りますか?"));

        //選択画面に戻るボタン
        selectButton.OnClickAsObservable()
            .Subscribe(_ => SetMoveSceneButton(GameState.Start, "選択画面に戻りますか?"));
        //ゲーム終了画面表示
        gameEndButoon.OnClickAsObservable()
            .Subscribe(_ => SetMoveSceneButton(GameState.GameEnd, "ゲーム終了しますか?"));

        //音量設定画面表示
        soundButton.OnClickAsObservable()
            .Subscribe(_ => SetSoundButton());

        //はい
        yesButton.OnClickAsObservable()
            .Subscribe(_ => _pauseView.YesButton());

        //いいえ
        noButton.OnClickAsObservable()
            .Subscribe(_ => BackPauseMenu());

        //サウンドの閉じるボタン
        closeButton.OnClickAsObservable()
            .Subscribe(_ => BackPauseMenu());

        //閉じるボタン
        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Cancel"))
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Pause)
            .Subscribe(_ => BackPauseMenu());

    }

    //eventsystemに登録してボタンを押せるようにしている

    //シーン移動系ボタンを押した時の設定
    private void SetMoveSceneButton(GameState gameState, string text)
    {
        pushButton = eventSystem.currentSelectedGameObject.gameObject;
        _pauseView.SceneButton(gameState, text);
        eventSystem.SetSelectedGameObject(yesButton.gameObject);
    }

    //音量調整押した時の設定
    private void SetSoundButton()
    {
        pushButton = eventSystem.currentSelectedGameObject.gameObject;
        _pauseView.SoundButton();
        eventSystem.SetSelectedGameObject(closeButton.gameObject);
    }

    //閉じるボタンの設定
    private void BackPauseMenu()
    {
        _pauseView.CloseButton();
        if (pushButton != null)
        {
            eventSystem.SetSelectedGameObject(pushButton.gameObject);
        }
    }

}
