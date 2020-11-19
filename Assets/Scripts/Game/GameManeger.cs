using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : SingletonMonoBehaviour<GameManeger>
{
    public enum GameState
    {
        Opening,
        Start,
        Playing,
        GameOver
    }

    private GameState currentGameState;

    void Start()
    {
        currentGameState = GameState.Opening;
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
            case GameState.Playing:
                GamePlay();
                break;
            case GameState.GameOver:
                GameOver();
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

    private void GamePlay()//ゲーム画面
    {
        SceneManager.LoadScene("Play");
    }

    private void GameOver()//ゲームオーバー
    {
        SceneManager.LoadScene("GameOver");
    }

}
