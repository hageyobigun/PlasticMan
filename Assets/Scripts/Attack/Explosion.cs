using UnityEngine;
using UniRx;
using System;

public class Explosion : BaseAttack
{
    private Animator animator;
    [SerializeField] private float disappearTime = 1.0f;//何秒後削除するか

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("explosionTrigger");

        //n秒後削除
        Observable.Timer(TimeSpan.FromSeconds(disappearTime))
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(this);
    }

}
