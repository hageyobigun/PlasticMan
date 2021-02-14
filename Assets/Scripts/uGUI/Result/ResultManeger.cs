using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UniRx;

public class ResultManeger : SingletonMonoBehaviour<ResultManeger>
{
    [SerializeField] private ResultView _resultView = default;
    public ReactiveProperty<ResultState> resultStates = new ReactiveProperty<ResultState>();
    private bool isRushGame = false;    //bossrushかどうか
    private bool isRushClaer = false; //bossrushクリア

    // Start is called before the first frame update
    void Start()
    {

        if (GameManeger.Instance.currentGameStates.Value == GameState.RushGame)
        {
            isRushGame = true;
        }

        resultStates.Value = ResultState.Play;

        resultStates
            .Where(states => states == ResultState.Win)
            .Subscribe(_ => Win());

        resultStates
            .Where(states => states == ResultState.Lose)
            .Subscribe(_ => Lose());

    }

    //rushの最後だと知らせるもの
    public void IsRushClear()
    {
        isRushClaer = true;
    }

    public void Win()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.Result;
        if (isRushGame)
        {
            if (isRushClaer)
            {
                _resultView.RushGameClear();
            }
            else
            {
                _resultView.RushGameWin();
            }
        }
        else
        {
            _resultView.VsGameWin();
        }
    }


    public void Lose()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.Result;
        _resultView.GameLose();
    }
}
