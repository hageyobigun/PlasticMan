using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Character;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameObject fireBullet = null;
    [SerializeField] private GameObject bomb = null;
    [SerializeField] private GameObject barrier = null;
    private BaseEnemyAgent _baseEnemyAgent;


    public void Awake()
    {
        _baseEnemyAgent = GetComponent<BaseEnemyAgent>();
        //処理登録
        _baseEnemyAgent.attackObservable//通常弾
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                BulletAttack();
                _baseEnemyAgent.GetState = State.Bullet_Attack;
            });

        _baseEnemyAgent.attackObservable//炎
            .Where(attack => attack == 2 && _baseEnemyAgent.GetMpValue >= 3)
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ =>
                {
                    FireAttack();
                    _baseEnemyAgent.GetMpValue -= 3;
                    _baseEnemyAgent.GetState = State.Fire_Attack;
                });

        _baseEnemyAgent.attackObservable//爆弾
            .Where(attack => attack == 3 && _baseEnemyAgent.GetMpValue >= 4)
                .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
                .Subscribe(_ =>
                {
                    BombAttack();
                    _baseEnemyAgent.GetMpValue -= 4;
                    _baseEnemyAgent.GetState = State.Bomb_Attack;
                });

        _baseEnemyAgent.attackObservable//バリア
            .Where(attack => attack == 4 && _baseEnemyAgent.GetMpValue >= 5)
                .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
                .Subscribe(_ =>
                {
                    StartCoroutine(BarrierGuard());
                    _baseEnemyAgent.GetMpValue -= 5;
                    _baseEnemyAgent.GetState = State.Barrier;
                });

        if (barrier != null)barrier.SetActive(false);
    }

    public void BulletAttack()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }

    public void FireAttack()
    {
        Instantiate(fireBullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.Euler(0f, 0f, -90f));
    }

    public void BombAttack()
    {
        Instantiate(bomb, this.transform.position + new Vector3(0, 1.0f, -1.0f), Quaternion.identity);
    }

    public IEnumerator BarrierGuard()
    {
        barrier.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        barrier.SetActive(false);
    }
}
