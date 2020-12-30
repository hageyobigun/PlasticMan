using UnityEngine;
using UniRx;
using System;

public class Explosion : BaseAttack
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

}
