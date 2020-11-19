using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : SingletonMonoBehaviour<GameManeger>
{
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    }
    private GameState currentGameState;

    void Start()
    {
        currentGameState = GameState.Start;
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
            case GameState.Start:
                GameTitle();
                break;
            case GameState.Playing:
                GameStart();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            default:
                break;

        }
    }


    void GameTitle()
    {
        SceneManager.LoadScene("Start");
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Play");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
