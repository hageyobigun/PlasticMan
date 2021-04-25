using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UniRx;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private ResultView _resultView = default;
    [SerializeField] private EventSystemManeger _eventSystemManeger = default;
    [SerializeField] private GameObject firstButton = default; //eventsystemにセットするボタン

    private bool isRushGame = false;    //bossrushかどうか
    private bool isRushClaer = false; //bossrushクリア
    private Subject<Unit> winSubject = new Subject<Unit>();

    // Start is called before the first frame update
    void Start()
    {

        if (GameManeger.Instance.currentGameStates.Value == GameState.RushGame)//連戦かどうか
        {
            isRushGame = true;
        }

        //rushでクリアした場合
        winSubject
            .Where(_ => isRushGame)
            .Where(_ => isRushClaer)
            .Subscribe(_ => _resultView.RushGameClear());

        //rushで勝利した場合
        winSubject
            .Where(_ => isRushGame)
            .Where(_ => !isRushClaer)
            .Subscribe(_ => _resultView.RushGameWin());

        //vsで勝利した場合
        winSubject
            .Where(_ => !isRushGame)
            .Subscribe(_ => _resultView.VsGameWin());

    }

    //rushの最後だと知らせるもの
    public void IsRushClear() => isRushClaer = true;

    //負けた
    public void Lose()
    {
        _eventSystemManeger.FirstSetObj(firstButton);
        GameManeger.Instance.currentGameStates.Value = GameState.Result;
        _resultView.GameLose();
    }

    //勝利
    public void Win()
    {
        _eventSystemManeger.FirstSetObj(firstButton);
        GameManeger.Instance.currentGameStates.Value = GameState.Result;
        winSubject.OnNext(Unit.Default);
    }
}
