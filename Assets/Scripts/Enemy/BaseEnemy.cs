using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public abstract class BaseEnemy : MonoBehaviour, IDamageable, IAttackable
{

    [SerializeField] private int hpValue = 1;
    [SerializeField] private int _enemyPos = 4;
    private int[] moveList = { -3, -1, 1, 3};
    private MoveEnemy _moveEnemy;

    protected bool IsDead() => --hpValue <= 0;

    public abstract void Attacked();

    //いらない？
    public abstract void ApplyDamage();


    private void Awake()
    {
        _moveEnemy = new MoveEnemy(_enemyPos, gameObject);

        //移動
        Observable.Interval(TimeSpan.FromSeconds(0.4))
            .Where(_ => _moveEnemy.IsStage(moveList[UnityEngine.Random.Range(0, 4)]))
            .Subscribe(_ => _moveEnemy.Move())
            .AddTo(gameObject);
    }

}
