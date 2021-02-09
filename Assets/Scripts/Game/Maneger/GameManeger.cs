﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;
using Game;

public class GameManeger : SingletonMonoBehaviour<GameManeger>
{

    public ReactiveProperty<GameState> currentGameStates = new ReactiveProperty<GameState>();
    [SerializeField] private GameObject loadSceneImage = default;
    private int enemyNumber; //戦う敵の番号
    private MoveScene _moveScene;

    void Start()
    {
        enemyNumber = 0;
        DontDestroyOnLoad(this.gameObject);
        _moveScene = new MoveScene();
        SceneManager.sceneLoaded += SceneLoaded;

        //タイトル
        currentGameStates
            .Where(state => state == GameState.Title)
            .Skip(1)
            .Subscribe(_ => LoadScene("Title", "None"));

        //ゲーム選択画面
        currentGameStates
            .Where(state => state == GameState.Start)
            .Subscribe(_ =>
            {
                LoadScene("Start", "Select");
                Time.timeScale = 1.0f;
            });

        //ゲーム画面１vs１
        currentGameStates
            .Where(state => state == GameState.VsGame)
            .Subscribe(_ => LoadScene("Play", "None"));

        //ゲーム画面BossRush
        currentGameStates
            .Where(state => state == GameState.RushGame)
            .Subscribe(_ => LoadScene("Play", "None")); 

        //play
        currentGameStates
            .Where(state => state == GameState.Play)
            .Subscribe(_ =>
            {
                Time.timeScale = 1.0f;
                SoundManager.Instance.PlayBgm("Fight");
            });

        //pause
        currentGameStates
            .Where(state => state == GameState.Pause)
            .Subscribe(_ => Time.timeScale = 0f);
    }

    //シーン移動
    private void LoadScene(string sceneName, string bgmName)
    {
        var copyLoadSceneImage = Instantiate(loadSceneImage, loadSceneImage.transform.position, Quaternion.identity);

        //シーン移動演出
        Observable
            .FromCoroutine(() => _moveScene.LoadSceneImage(copyLoadSceneImage, true))
            .Subscribe(_ =>
            {
                SceneManager.LoadScene(sceneName);
                SoundManager.Instance.PlayBgm(bgmName);
            });
    }

    //シーン移動完了後
    private void OpenScene()
    {
        var copyLoadSceneImage = Instantiate(loadSceneImage, loadSceneImage.transform.position, Quaternion.identity);
        StartCoroutine(_moveScene.LoadSceneImage(copyLoadSceneImage, false));
    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        OpenScene();
    }

    public int GetEnemyNumber
    {
        get { return this.enemyNumber; }  //取得用
        set { this.enemyNumber = value; } //値入力用
    }
}
