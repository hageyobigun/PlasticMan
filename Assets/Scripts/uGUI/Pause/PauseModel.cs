using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UniRx;

public class PauseModel
{
    public ReactiveProperty<GameState> pauseState;

    public PauseModel()
    {
        pauseState = new ReactiveProperty<GameState>();
        pauseState.Value = GameState.Play;
    }


    public void ChangePauseState()
    {
        if (pauseState.Value == GameState.Play)
        {
            pauseState.Value = GameState.Pause;
            GameManeger.Instance.currentGameStates.Value = GameState.Pause;
        }
        else if (pauseState.Value == GameState.Pause)
        {
            pauseState.Value = GameState.Play;
            GameManeger.Instance.currentGameStates.Value = GameState.Play;
        }
    }
}
