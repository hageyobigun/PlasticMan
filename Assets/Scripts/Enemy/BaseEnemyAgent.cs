﻿using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;

public abstract class BaseEnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;
    private int[] moveList = { -3, -1, 1, 3, 0};
    public GameObject player;

    private EnemyMove _enemyMove;
    [SerializeField] private LifePresenter _lifePresenter = null;

    private Subject<int> attackSubject = new Subject<int>();
    public IObservable<int> attackObservable { get { return attackSubject; }}

    private Subject<int> moveSubject = new Subject<int>();
    private State enemyState;

    protected PlayerAgent _playerAgent;
    protected PlayerController _playerController;

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(4, gameObject);
        _playerAgent = player.GetComponent<PlayerAgent>();
        _playerController = player.GetComponent<PlayerController>();

        moveSubject
            .Where(move => _enemyMove.IsStage(moveList[move]))
            .Subscribe(_ => _enemyMove.Move());
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _lifePresenter.Initialize(hpValue, mpValue);
        enemyState = State.Normal;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);
        if (GetHpValue <= 0)//死亡
        {
            EndEpisode();
        }

        if (player != null)
        {
            if (_playerController != null)//切り替え可能
            {
                if (_playerController.GetHpValue <= 0) //撃破
                {
                    AddReward(1.0f);
                    EndEpisode();
                }
            }
        }
    }

    public virtual void Attacked(float damage)
    {
        hpValue -= (int)damage;
        _lifePresenter.OnChangeHpLife(hpValue);
        if (_playerController != null && hpValue <= 0)
        {
            GameManeger.Instance.SetCurrentState(GameManeger.GameState.Win);
            Destroy(gameObject);
        }
    }

    //プロパティー
    public int GetHpValue
    {
        get { return this.hpValue; }  //取得用
        set { this.hpValue = value; } //値入力用
    }

    //プロパティー
    public int GetMpValue
    {
        get { return this.mpValue; }  //取得用
        set { this.mpValue = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.enemyState; }  //取得用
        set { this.enemyState = value; } //値入力用
    }
}
