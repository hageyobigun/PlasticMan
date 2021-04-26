using System;
using Character;
using UniRx;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Mp;

public class EnduranceEnemyAgent : BaseEnemyAgent
{
    [SerializeField] private float oneStepReward = 0; //1stepの報酬の大きさ

    public override void Initialize()
    {
        base.Initialize();
        attackObservable//通常弾
            .Where(attack => attack == 1 && MpValue >= Attack.bulletMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.bulletInterval))
            .Subscribe(_ =>
            {
                _attackManager.BulletAttack();
                MpAction(Attack.bulletMp, "Attack");
                AttackState = State.Bullet_Attack;
            });

        attackObservable//炎
            .Where(attack => attack == 2 && MpValue >= Attack.firetMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.firetInterval))
            .Subscribe(_ =>
            {
                _attackManager.FireAttack();
                MpAction(Attack.firetMp, "Attack");
                AttackState = State.Fire_Attack;
            });

        attackObservable//バリア
            .Where(attack => attack == 3 && MpValue >= Attack.barrierMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.barrierInterval))
            .Subscribe(_ =>
            {
                BarrierInterVal();
                GuardState = State.Barrier;
            });
    }

    /*
     * 観察対象 個数:11(自分の位置(3),自分のHP(1), 自分のMP(1), 自分の防御状態(1), 自分の攻撃状態(1),
     * playerの位置(3), playerの攻撃状態(1))
    */
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(HpValue);
        sensor.AddObservation(MpValue);
        sensor.AddObservation((float)GuardState);
        sensor.AddObservation((float)AttackState);

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
        if (GuardState != State.Barrier)//防御中は無効化
        {
            base.Attacked(damage);
            if (HpValue <= 0)//死亡(学習中）
            {
                AddReward(StepCount * oneStepReward); //生きている時間(ステップ数)が長いほど高い報酬を与える
                EndEpisode();
            }
        }
    }

    //バリアを呼び出し終了後に処理
    public void BarrierInterVal()
    {
        Observable.FromCoroutine(() => _attackManager.BarrierGuard(AttackInterval.barrierInterval))
            .Subscribe(_ => GuardState = State.Normal)
            .AddTo(this);
    }
}
