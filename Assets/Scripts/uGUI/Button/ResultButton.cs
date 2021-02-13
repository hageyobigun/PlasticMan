using UnityEngine;
using Game;
using UniRx;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour
{
    [SerializeField] private Button retryBuuton = default;
    [SerializeField] private Button titleButton = default;
    [SerializeField] private Button selectButton = default;
    [SerializeField] private Button gameEndButoon = default;

    // Start is called before the first frame update
    void Start()
    {

        //リトライボタン
        retryBuuton.OnClickAsObservable()
            .Subscribe(_ => MoveSceneButton(GameState.Retry));//どうするか悩み中)

        //タイトルに戻るボタン
        titleButton.OnClickAsObservable()
            .Subscribe(_ => MoveSceneButton(GameState.Title));

        //選択画面に戻るボタン
        selectButton.OnClickAsObservable()
            .Subscribe(_ => MoveSceneButton(GameState.Start));


        //ゲーム終了画面表示
        gameEndButoon.OnClickAsObservable()
            .Subscribe(_ => MoveSceneButton(GameState.GameEnd));
    }

    //シーン移動するボタン(リトライ、タイトル、選択画面)
    public void MoveSceneButton(GameState gameState)
    {
        GameManeger.Instance.currentGameStates.Value = gameState;
        SoundManager.Instance.PlaySe("NormalButton");
    }

}
