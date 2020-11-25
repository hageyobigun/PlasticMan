using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public abstract class BaseEnemy : MonoBehaviour, Enemy.IAttackable
{

    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;
    [SerializeField] private int _enemyPos = 4;
    private int[] moveList = { -3, -1, 1, 3};
    private EnemyMove _enemyMove;
    private EnemyAttacks _enemyAttacks;
    protected SliderModel _sliderModel;

    protected bool IsDead() => _sliderModel.hp.Value <= 0;

    public abstract void Attacked(float damage);

    private void Awake()
    {
        _enemyMove = new EnemyMove(_enemyPos, gameObject);
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _sliderModel = GetComponent<SliderModel>();
        _sliderModel.Initialize(hpValue, mpValue);


        //移動
        Observable.Interval(TimeSpan.FromSeconds(0.4))
            .Where(_ => _enemyMove.IsStage(moveList[UnityEngine.Random.Range(0, 4)]))
            .Subscribe(_ => _enemyMove.Move())
            .AddTo(gameObject);

        //攻撃
        Observable.Interval(TimeSpan.FromSeconds(0.5))
            .Subscribe(_ => _enemyAttacks.BulletAttack())
            .AddTo(gameObject);

        //攻撃
        Observable.Interval(TimeSpan.FromSeconds(1.1))
            .Where(_ => _sliderModel.mp.Value >= 3)
            .Subscribe(_ => {
                _enemyAttacks.BombAttack();
                _sliderModel.mp.Value -= 3;
            })
            .AddTo(gameObject);

        //攻撃
        Observable.Interval(TimeSpan.FromSeconds(1.6))
            .Where(_ => _sliderModel.mp.Value >= 4)
            .Subscribe(_ => {
                _enemyAttacks.FireAttack();
                _sliderModel.mp.Value -= 4;
            })
            .AddTo(gameObject);
    }

}
