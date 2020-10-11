using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public abstract class BaseEnemy : MonoBehaviour, IDamageable, IAttackable
{

    [SerializeField] private int hpValue = 1;
    //[SerializeField] private GameObject enemyStage = null;
    //private PlayerStage _playerStage;

    protected bool IsDead() => --hpValue <= 0;

    public abstract void Attacked();

    //いらない？
    public abstract void ApplyDamage();

    //やり方悩み中
    public virtual void EnemyMove()
    {

    }

    //private void Awake()
    //{
    //    _playerStage = new PlayerStage(enemyStage, 4);
    //}

}
