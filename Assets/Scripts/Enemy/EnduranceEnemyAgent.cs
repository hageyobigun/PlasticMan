using System;
using Character;
using UniRx;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class EnduranceEnemyAgent : BaseEnemyAgent
{
    [SerializeField] private float barrierInterval = 1.0f;
    [SerializeField] private float oneStepReward = 0; //1stepの報酬の大きさ

    public override void Initialize()
    {
        base.Initialize();
        attackObservable//通常弾
            .Where(attack => attack == 1)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _enemyAttacks.BulletAttack();
                GetAttackState = State.Bullet_Attack;
            });

        attackObservable//炎
            .Where(attack => attack == 2 && GetMpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _enemyAttacks.FireAttack();
                MpConsumption(3);
                GetAttackState = State.Fire_Attack;
            });

        attackObservable//バリア
            .Where(attack => attack == 3 && GetMpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(barrierInterval))
            .Subscribe(_ =>
            {
                BarrierInterVal();
                MpConsumption(5);
                GetGuardState = State.Barrier;
            });
    }

    /*
     * 観察対象 個数:11(自分の位置(3),自分のHP(1), 自分のMP(1), 自分の防御状態(1), 自分の攻撃状態(1),
     * playerの位置(3), playerの攻撃状態(1))
    */
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(GetHpValue);
        sensor.AddObservation(GetMpValue);
        sensor.AddObservation((float)GetGuardState);
        sensor.AddObservation((float)GetAttackState);

        if (player != null)
        {
            sensor.AddObservation(player.transform.position);
            if (_playerAgent != null)
            {
                sensor.AddObservation((float)_playerAgent.GetAttackState);
            }
            else if (_playerController != null)
            {
                sensor.AddObservation((float)_playerController.GetAttackState);
            }
        }
        else //playerが消滅(撃破)した際のバグ対策
        {
            sensor.AddObservation(this.transform.position);
            sensor.AddObservation((float)State.Normal);
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);//攻撃　0:なし　1:通常弾　2:炎 3:バリア

        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue <= 0) //撃破
            {
                AddReward(0.1f + (StepCount * oneStepReward)); //生きている時間(ステップ数)が長いほど高い報酬を与える
                EndEpisode();
            }
        }
    }

    //ダメージ処理
    public override void Attacked(float damage)
    {
        if (GetGuardState != State.Barrier)//防御中は無効化
        {
            base.Attacked(damage);
            if (GetHpValue <= 0)//死亡(学習中）
            {
                AddReward(StepCount * oneStepReward); //生きている時間(ステップ数)が長いほど高い報酬を与える
                EndEpisode();
            }
        }
    }

    //バリアを呼び出し終了後に処理
    public void BarrierInterVal()
    {
        Observable.FromCoroutine(() => _enemyAttacks.BarrierGuard(barrierInterval))
            .Subscribe(_ => GetGuardState = State.Normal)
            .AddTo(this);
    }
}
