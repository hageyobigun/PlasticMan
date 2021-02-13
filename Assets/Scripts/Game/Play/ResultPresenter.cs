﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UniRx;
using UniRx.Triggers;

public class ResultPresenter : MonoBehaviour
{
    private ResultView _resultView;
    private bool isRushGame = false;

    // Start is called before the first frame update
    void Start()
    {
        _resultView = GetComponent<ResultView>();
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

        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.RushClear)
            .Subscribe(_ => _resultView.RushGameClear());

    }

    public void Win()
    {
        if (isRushGame == true)
        {
            _resultView.RushGameWin();
        }
        else
        {
            _resultView.VsGameWin();
        }
    }
    

    public void Lose()
    {
        _resultView.GameLose();
    }
}
