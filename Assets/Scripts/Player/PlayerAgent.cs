using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;

public class PlayerAgent : Agent , Player.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;
    [SerializeField] private GameObject enemy = null;

    private int[] moveList = { -3, -1, 1, 3, 0};

    private PlayerMove _playerMove;
    private PlayerStage _playerStage;
    private PlayerAttack _playerAttack;
    private BaseEnemyAgent _enemyAgent;
    private SliderModel _sliderModel;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<int> moveSubject = new Subject<int>();

    private State playerState;

    //プロパティー
    public int GetHpValue
    {
        get { return this._sliderModel.hp.Value; }  //取得用
        private set { this._sliderModel.hp.Value = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.playerState; }  //取得用
        private set { this.playerState = value; } //値入力用
    }

    public override void Initialize()
    {
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(4);
        _playerAttack = GetComponent<PlayerAttack>();
        _enemyAgent = enemy.GetComponent<BaseEnemyAgent>();
        _sliderModel = GetComponent<SliderModel>();

        attackSubject
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _playerAttack.BulletAttack();
                playerState = State.Bullet_Attack;
            });

        attackSubject
            .Where(attack => attack == 2 && _sliderModel.mp.Value >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _playerAttack.FireAttack();
                _sliderModel.mp.Value -= 3;
                playerState = State.Fire_Attack;
            });

        attackSubject
            .Where(attack => attack == 3 && _sliderModel.mp.Value >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _playerAttack.BombAttack();
                _sliderModel.mp.Value -= 4;
                playerState = State.Bomb_Attack;
            });

        attackSubject
            .Where(attack => attack == 4 && _sliderModel.mp.Value >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_playerAttack.BarrierGuard());
                _sliderModel.mp.Value -= 5;
                playerState = State.Barrier;
            });

        moveSubject
            .Where(move => _playerStage.IsStage(moveList[move]))
            .Subscribe(_ => _playerMove.Move(_playerStage.getPlayerPos));
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _sliderModel.Initialize(hpValue, mpValue);
        playerState = State.Normal;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(enemy.transform.position);
        sensor.AddObservation(_sliderModel.hp.Value);
        sensor.AddObservation(_sliderModel.mp.Value);
        sensor.AddObservation((float)playerState);
        if (_enemyAgent != null)
        {
            sensor.AddObservation((float)_enemyAgent.GetState);
        }
        else
        {
            sensor.AddObservation(-1);
        }
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
        if (_sliderModel.hp.Value < 0)
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
