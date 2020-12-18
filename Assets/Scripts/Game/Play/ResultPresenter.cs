using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UniRx;
using UniRx.Triggers;

public class ResultPresenter : MonoBehaviour
{
    private ResultView resultView;
    private bool isRushGame = false;

    // Start is called before the first frame update
    void Start()
    {
        resultView = GetComponent<ResultView>();
        if (GameManeger.Instance.currentGameStates.Value == GameState.RushGame)
        {
            isRushGame = true;
        }

        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Win)
            .Subscribe(_ => Win());

        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Lose)
            .Subscribe(_ => Lose());

    }


    public void Win()
    {
        if (isRushGame == true)
        {
            resultView.RushGameWin();
        }
        else
        {
            resultView.VsGameWin();
        }
    }

    public void Lose()
    {
        if (isRushGame == true)
        {
            resultView.RushGameLose();
        }
        else
        {
            resultView.VsGameLose();
        }
    }

}
