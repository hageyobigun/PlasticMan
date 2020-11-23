using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;

public class EnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;
    [SerializeField] private GameObject player = null;
    private int[] moveList = { -3, -1, 1, 3, 0};

    private EnemyMove _enemyMove ;
    private EnemyAttacks _enemyAttacks;
    private PlayerAgent _playerAgent;
    private PlayerController _playerController;
    private SliderModel _sliderModel;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<int> moveSubject = new Subject<int>();

    private State enemyState;

    //プロパティー
    public int GetHpValue
    {
        get { return this._sliderModel.hp.Value; }  //取得用
        private set { this._sliderModel.hp.Value = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.enemyState; }  //取得用
        private set { this.enemyState = value; } //値入力用
    }

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(4, gameObject);
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _playerAgent = player.GetComponent<PlayerAgent>();
        _playerController = player.GetComponent<PlayerController>();
        _sliderModel = GetComponent<SliderModel>();

        attackSubject
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BulletAttack();
                enemyState = State.Bullet_Attack;
            });

        attackSubject
            .Where(attack => attack == 2 && _sliderModel.mp.Value >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.FireAttack();
                _sliderModel.mp.Value -= 3;
                enemyState = State.Fire_Attack;
            });

        attackSubject
            .Where(attack => attack == 3 && _sliderModel.mp.Value >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BombAttack();
                _sliderModel.mp.Value -= 4;
                enemyState = State.Bomb_Attack;
            });

        attackSubject
            .Where(attack => attack == 4 && _sliderModel.mp.Value >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_enemyAttacks.BarrierGuard());
                _sliderModel.mp.Value -= 5;
                enemyState = State.Barrier;
            });

        moveSubject
            .Where(move => _enemyMove.IsStage(moveList[move]))
            .Subscribe(_ =>
            {
                _enemyMove.Move();
            });
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _sliderModel.Initialize(hpValue, mpValue);
        enemyState = State.Normal;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(player.transform.position);
        sensor.AddObservation(_sliderModel.hp.Value);
        sensor.AddObservation(_sliderModel.mp.Value);
        if (_playerAgent != null)
        {
            sensor.AddObservation((float)_playerAgent.GetState);
        }
        else if (_playerController != null)
        {
            sensor.AddObservation((float)_playerController.GetState);
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);
        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue <= 0) //撃破
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

        else if (_playerController != null)//切り替え可能
        {
            if (_playerController.GetHpValue <= 0) //撃破
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

        if (_sliderModel.hp.Value < 0)//死亡
        {
            EndEpisode();
        }
    }


    public void Attacked(float damage)
    {
        _sliderModel.hp.Value -= (int)damage;
        AddReward(damage * 0.01f);
    }
}
