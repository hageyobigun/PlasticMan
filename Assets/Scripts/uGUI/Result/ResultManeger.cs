using UnityEngine;
using Game;
using UniRx;
using UnityEngine.EventSystems;

public class ResultManeger : SingletonMonoBehaviour<ResultManeger>
{
    [SerializeField] private ResultView _resultView = default;
    [SerializeField] private EventSystem eventSystem = default;
    [SerializeField] private GameObject firstButton = default;

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
        eventSystem.SetSelectedGameObject(firstButton);
        GameManeger.Instance.currentGameStates.Value = GameState.Result;
        _resultView.GameLose();
    }

    //勝利
    public void Win()
    {
        eventSystem.SetSelectedGameObject(firstButton);
        GameManeger.Instance.currentGameStates.Value = GameState.Result;
        winSubject.OnNext(Unit.Default);
    }

}
