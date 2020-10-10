using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamageable, IAttackable
{

    [SerializeField] private int hpValue = 1;

    protected bool IsDead() => --hpValue <= 0;

    public abstract void Attacked();

    public abstract void ApplyDamage();

    public virtual void EnemyMove()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
