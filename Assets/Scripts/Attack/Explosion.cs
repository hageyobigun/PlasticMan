using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Explosion : MonoBehaviour
{
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("explosionTrigger");

        //n秒後削除
        Observable.Timer(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var attackable_player = collision.GetComponent<Player.IAttackable>();
        var attackable_enemy = collision.GetComponent<Enemy.IAttackable>();
        if (attackable_player != null)
        {
            attackable_player.Attacked(2);
        }

        if (attackable_enemy != null)
        {
            attackable_enemy.Attacked(2);
        }

        var attacknotable = collision.GetComponent<IAttacknotable>();
        if (attacknotable != null)
        {
            Destroy(gameObject);
        }
    }
}
