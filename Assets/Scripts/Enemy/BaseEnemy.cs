using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public abstract class BaseEnemy : MonoBehaviour, Enemy.IAttackable
{

    [SerializeField] private int hpValue = 1;
    [SerializeField] private int _enemyPos = 4;
    private int[] moveList = { -3, -1, 1, 3};
    private EnemyMove _enemyMove;
    private EnemyAttacks _enemyAttacks;

    protected bool IsDead() => --hpValue <= 0;

    public abstract void Attacked(float damage);

    private void Awake()
    {
        _enemyMove = new EnemyMove(_enemyPos, gameObject);
        _enemyAttacks = GetComponent<EnemyAttacks>();

        //移動
        Observable.Interval(TimeSpan.FromSeconds(0.4))
            .Where(_ => _enemyMove.IsStage(moveList[UnityEngine.Random.Range(0, 4)]))
            .Subscribe(_ => _enemyMove.Move())
            .AddTo(gameObject);

        //移動
        Observable.Interval(TimeSpan.FromSeconds(0.4))
            .Subscribe(_ => _enemyAttacks.BulletAttack())
            .AddTo(gameObject);
    }

}
