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

    private int enemyNumber; //戦う敵の番号


    void Start()
    {
        enemyNumber = 0;
        DontDestroyOnLoad(this.gameObject);

        //タイトル
        currentGameStates
            .Where(state => state == GameState.Opening)
            .Subscribe(_ => SceneManager.LoadScene("Title"));

        //ゲーム選択画面
        currentGameStates
            .Where(state => state == GameState.Start)
            .Subscribe(_ => SceneManager.LoadScene("Start"));

        //ゲーム画面１vs１
        currentGameStates
            .Where(state => state == GameState.VsGame)
            .Subscribe(_ => SceneManager.LoadScene("Play"));

        //ゲーム画面BossRush
        currentGameStates
            .Where(state => state == GameState.RushGame)
            .Subscribe(_ => SceneManager.LoadScene("Play"));
    }


    public int GetEnemyNumber
    {
        get { return this.enemyNumber; }  //取得用
        set { this.enemyNumber = value; } //値入力用
    }
}
