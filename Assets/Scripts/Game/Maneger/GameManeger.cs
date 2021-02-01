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
    [SerializeField] private GameObject canvas = default;
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
            .Subscribe(_ =>
            {
                //SceneManager.LoadScene("Title");
                Scene("Title");
                SoundManager.Instance.StopBgm();
            });

        //ゲーム選択画面
        currentGameStates
            .Where(state => state == GameState.Start)
            .Subscribe(_ =>
            {
                //SceneManager.LoadScene("Start");
                Scene("Start");
                SoundManager.Instance.PlayBgm("Select");
                Time.timeScale = 1.0f;
            });

        //ゲーム画面１vs１
        currentGameStates
            .Where(state => state == GameState.VsGame)
            .Subscribe(_ =>
            {
                Scene("Play");
                //SceneManager.LoadScene("Play");
                SoundManager.Instance.StopBgm();
            });

        //ゲーム画面BossRush
        currentGameStates
            .Where(state => state == GameState.RushGame)
            .Subscribe(_ =>
            {
                Scene("Play");
                //SceneManager.LoadScene("Play");
                SoundManager.Instance.StopBgm();
            }); 

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

    private void Scene(string sceneName)
    {
        var copy = Instantiate(canvas, canvas.transform.position, Quaternion.identity);

        Observable
            .FromCoroutine(() => _moveScene.OpenBlackBlock(copy))
            .Subscribe(_ =>
            {
                SceneManager.LoadScene(sceneName);
            });
    }

    private void OpenScene()
    {
        var copy = Instantiate(canvas, canvas.transform.position, Quaternion.identity);
        Observable
            .FromCoroutine(() => _moveScene.CloseBlackBlock(copy))
            .Subscribe(_ =>
            {
            });
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
