using System;
using Character;
using UniRx;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class EnduranceEnemyAgent : BaseEnemyAgent
{
    private Subject<Unit> BarrierSubject = new Subject<Unit>();

    public override void Initialize()
    {
        base.Initialize();
        attackObservable//通常弾
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BulletAttack();
                //GetState = State.Bullet_Attack;
            });

        attackObservable//炎
            .Where(attack => attack == 2 && GetMpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.FireAttack();
                MpConsumption(3);
                //GetState = State.Fire_Attack;
            });

        attackObservable//バリア
            .Where(attack => attack == 3 && GetMpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_enemyAttacks.BarrierGuard());
                MpConsumption(5);
                GetState = State.Barrier;
                BarrierSubject.OnNext(Unit.Default);
            });

        BarrierSubject
            .Delay(TimeSpan.FromSeconds(2.0f))
            .Subscribe(_ => GetState = State.Normal);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(GetHpValue);
        sensor.AddObservation(GetMpValue);
        sensor.AddObservation((float)GetState);
        if (player != null)
        {
            sensor.AddObservation(player.transform.position);
            if (_playerAgent != null) sensor.AddObservation((float)_playerAgent.GetState);
            else if (_playerController != null) sensor.AddObservation((float)_playerController.GetState);
        }
        else
        {
            sensor.AddObservation(this.transform.position);
            sensor.AddObservation(0);
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);//攻撃　0:なし　1:通常弾　2:炎 3:バリア

        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue <= 0) //撃破
            {
                AddReward(0.1f);
                EndEpisode();
            }
        }

        if (StepCount % 1000 == 0)
        {
            AddReward(0.1f);
        }
    }

    

    public override void Attacked(float damage)
    {
        base.Attacked(damage);
    }
}
