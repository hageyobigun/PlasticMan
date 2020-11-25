using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyAgent : BaseEnemyAgent
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);

        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue <= 0) //撃破
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

        else if (_playerController != null)//切り替え可能
        {
            if (_playerController.GetHpValue <= 0) //撃破
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

        if (GetHpValue < 0)//死亡
        {
            EndEpisode();
        }

    }

    public override void Attacked(float damage)
    {
        base.Attacked(damage);
        AddReward(damage * 0.01f);
    }
}
