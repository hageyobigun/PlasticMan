using UnityEngine;

public class AttackEnemyAgent : BaseEnemyAgent
{
    private int playerHp;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        if (_playerAgent != null)
        {
            playerHp = _playerAgent.GetHpValue;
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);

        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue < playerHp)
            {
                AddReward(((float) playerHp - _playerAgent.GetHpValue) * 0.01f);
                playerHp = _playerAgent.GetHpValue;
            }
            if (_playerAgent.GetHpValue <= 0) //撃破
            {
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
    }
}
