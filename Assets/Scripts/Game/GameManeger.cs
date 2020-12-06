using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class GameManeger : SingletonMonoBehaviour<GameManeger>
{
    public enum GameState
    {
        Opening,
        Start,
        Playing_Vs,
        Win,
        Lose
    }

    private GameState currentGameState;
    private Subject<GameState> gameStateSubjet = new Subject<GameState>();
    public IObservable<GameState> gameStateChanged{get { return gameStateSubjet; }}

    public int enemyNumber;


    void Start()
    {
        currentGameState = GameState.Opening;
        enemyNumber = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetCurrentState(GameState state)
    {
        currentGameState = state;
        OnGameStateChanged(currentGameState);
    }

    // 状態が変わったら何をするか
    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Opening:
                GameTitle();
                break;
            case GameState.Start:
                GameStart();
                break;
            case GameState.Playing_Vs:
                GamePlayVs();
                break;
            case GameState.Win:
                GameWin();
                break;
            case GameState.Lose:
                GameLose();
                break;
            default:
                break;

        }
    }

    private void GameTitle()//タイトル画面
    {
        SceneManager.LoadScene("Title");
    }

    private void GameStart()//選択画面
    {
        SceneManager.LoadScene("Start");
    }

    private void GamePlayVs()//ゲーム画面
    {
        gameStateSubjet = new Subject<GameState>();
        SceneManager.LoadScene("Play");
    }

    private void GameWin()//ゲームオーバー
    {
        gameStateSubjet.OnNext(currentGameState);
    }

    private void GameLose()//ゲームオーバー
    {
        gameStateSubjet.OnNext(currentGameState);
    }
}
