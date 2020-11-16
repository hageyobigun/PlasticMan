using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;

public class EnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;

    [SerializeField] private GameObject player = null;
    private int[] moveList = { -3, -1, 1, 3, 0};

    private EnemyMove _enemyMove ;
    private EnemyAttacks _enemyAttacks;
    private PlayerAgent _playerAgent;
    private SliderModel _sliderModel;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<int> moveSubject = new Subject<int>();

    //プロパティー
    public int GetHpValue
    {
        get { return this._sliderModel.hp.Value; }  //取得用
        private set { this._sliderModel.hp.Value = value; } //値入力用
    }

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(4, gameObject);
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _playerAgent = player.GetComponent<PlayerAgent>();
        _sliderModel = GetComponent<SliderModel>();

        attackSubject
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ => _enemyAttacks.BulletAttack());

        attackSubject
            .Where(attack => attack == 2 && _sliderModel.mp.Value >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.FireAttack();
                _sliderModel.mp.Value -= 3;
            });

        attackSubject
            .Where(attack => attack == 3 && _sliderModel.mp.Value >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BombAttack();
                _sliderModel.mp.Value -= 4;
            });

        attackSubject
            .Where(attack => attack == 4 && _sliderModel.mp.Value >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_enemyAttacks.BarrierGuard());
                _sliderModel.mp.Value -= 5;
            });

        moveSubject
            .Where(move => _enemyMove.IsStage(moveList[move]))
            .ThrottleFirst(TimeSpan.FromSeconds(0.2f))
            .Subscribe(_ => _enemyMove.Move());
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _sliderModel.Initialize(hpValue, mpValue);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(player.transform.position);
        sensor.AddObservation(_sliderModel.hp.Value);
        sensor.AddObservation(_sliderModel.mp.Value);
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

        if (_sliderModel.hp.Value < 0)//死亡
        {
            AddReward(0.2f);
            EndEpisode();
        }
    }


    public void Attacked(float damage)
    {
        _sliderModel.hp.Value -= (int)damage;
    }
}
