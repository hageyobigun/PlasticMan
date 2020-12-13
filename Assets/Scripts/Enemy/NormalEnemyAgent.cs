﻿using System;
using Character;
using UniRx;
using Unity.MLAgents.Sensors;

public class NormalEnemyAgent : BaseEnemyAgent
{
    public override void Initialize()
    {
        base.Initialize();
        attackObservable//通常弾
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BulletAttack();
                GetState = State.Bullet_Attack;
            });

        attackObservable//炎
            .Where(attack => attack == 2 && GetMpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.FireAttack();
                MpConsumption(3);
                GetState = State.Fire_Attack;
            });
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
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
        base.OnActionReceived(vectorAction);//攻撃　0:なし　1:通常弾　2:炎

        if (_playerAgent != null) //撃破
        {
            if (_playerAgent.GetHpValue <= 0)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

    }

    public override void Attacked(float damage)
    {
        base.Attacked(damage);
        if (GetHpValue <= 0)//死亡
        {
            AddReward(-0.1f);
        }

    }
}
