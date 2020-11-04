using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;

public class EnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int death = 0 ;

    [SerializeField] private int hpValue = 1;
    [SerializeField] private int maxMpValue = 100;
    private int mpValue;

    [SerializeField] private int _enemyPos = 4;
    [SerializeField] private GameObject player = null;
    private int[] moveList = { -3, -1, 1, 3, 0};

    private EnemyMove _enemyMove ;
    private EnemyAttacks _enemyAttacks;
    private PlayerAgent _playerAgent;

    protected bool IsDead() => --hpValue <= 0;

    //イベントを発行する核となるインスタンス
    private Subject<int> attackSubject = new Subject<int>();

    //イベントの購読側だけを公開
    public IObservable<int> OnAttackAction
    {
        get { return attackSubject; }
    }

    //プロパティー
    public int GetHpValue
    {
        get { return this.hpValue; }  //取得用
        private set { this.hpValue = value; } //値入力用
    }

    public void Attacked(float damage)
    {
        IsDead();
    }

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(_enemyPos, gameObject);
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _playerAgent = player.GetComponent<PlayerAgent>();

        attackSubject
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ => _enemyAttacks.BulletAttack());

        attackSubject
            .Where(attack => attack == 2 && mpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.FireAttack();
                mpValue -= 3;
            });

        attackSubject
            .Where(attack => attack == 3 && mpValue >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BombAttack();
                mpValue -= 4;
            });

        attackSubject
            .Where(attack => attack == 4 && mpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_enemyAttacks.BarrierGuard());
                mpValue -= 5;
            });

    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        hpValue = 10;
        mpValue = maxMpValue;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(player.transform.position);
        sensor.AddObservation(mpValue);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        bool isMove = _enemyMove.IsStage(moveList[move]);
        attackSubject.OnNext(attack);

        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue <= 0)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }
        if (isMove)
        {
            _enemyMove.Move();
        }

        if (hpValue < 0)
        {
            AddReward(0.2f);
            death++;
            EndEpisode();
        }
        mpValue++;
    }

}
