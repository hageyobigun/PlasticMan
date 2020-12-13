using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;
using Game;

public class GameManeger : SingletonMonoBehaviour<GameManeger>
{

    public ReactiveProperty<GameState> currentGameStates = new ReactiveProperty<GameState>();

    public int enemyNumber;


    void Start()
    {
        enemyNumber = 0;
        DontDestroyOnLoad(this.gameObject);

        currentGameStates
            .Where(state => state == GameState.Opening)
            .Subscribe(_ => SceneManager.LoadScene("Title"));

        currentGameStates
            .Where(state => state == GameState.Start)
            .Subscribe(_ => SceneManager.LoadScene("Start"));

        currentGameStates
            .Where(state => state == GameState.VsGame)
            .Subscribe(_ => SceneManager.LoadScene("Play"));

        currentGameStates
            .Where(state => state == GameState.RushGame)
            .Subscribe(_ => SceneManager.LoadScene("Play"));
    }
}
