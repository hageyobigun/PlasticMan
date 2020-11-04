using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;

public class PlayerAgent : Agent , Player.IAttackable
{
    [SerializeField] private int death = 0;

    [SerializeField] private int hpValue = 1;
    [SerializeField] private GameObject enemy = null;
    [SerializeField] private int maxMpValue = 100;
    private int mpValue;

    private int[] moveList = { -3, -1, 1, 3, 0};

    private PlayerMove _playerMove;
    private PlayerStage _playerStage;
    private PlayerAttack _playerAttack;

    private EnemyAgent _enemyAgent;

    protected bool IsDead() => --hpValue <= 0;

    //イベントを発行する核となるインスタンス
    private Subject<int> attackSubject = new Subject<int>();

    //イベントの購読側だけを公開
    public IObservable<int> OnAttackAction
    {
        get { return attackSubject; }
    }

    private Subject<Unit> mpSubject = new Subject<Unit>();

    private Subject<int> moveSubject = new Subject<int>();

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
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(4);
        _playerAttack = GetComponent<PlayerAttack>();
        _enemyAgent = enemy.GetComponent<EnemyAgent>();

        attackSubject
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ => _playerAttack.BulletAttack());

        attackSubject
            .Where(attack => attack == 2 && mpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _playerAttack.FireAttack();
                mpValue -= 3;
            });

        attackSubject
            .Where(attack => attack == 3 && mpValue >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _playerAttack.BombAttack();
                mpValue -= 4;
            });

        attackSubject
            .Where(attack => attack == 4 && mpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_playerAttack.BarrierGuard());
                mpValue -= 5;
            });

        mpSubject
            .Where(_ => mpValue <= maxMpValue)
            .ThrottleFirst(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => mpValue++);

        moveSubject
            .Where(move => _playerStage.IsStage(moveList[move]))
            .ThrottleFirst(TimeSpan.FromSeconds(0.2f))
            .Subscribe(_ => _playerMove.Move(_playerStage.getPlayerPos));
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
        sensor.AddObservation(enemy.transform.position);
        sensor.AddObservation(mpValue);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);

        if (_enemyAgent != null)
        {
            if (_enemyAgent.GetHpValue <= 0)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }
        if (hpValue < 0)
        {
            AddReward(0.2f);
            death++;
            EndEpisode();
        }
        mpSubject.OnNext(Unit.Default);
    }

}
