using Unity.MLAgents.Sensors;
using UnityEngine;

public class AttackEnemyAgent : BaseEnemyAgent
{

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(GetHpValue);
        sensor.AddObservation(GetMpValue);
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
        sensor.AddObservation((float)GetState);
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
    }

    public override void Attacked(float damage)
    {
        base.Attacked(damage);
    }
}
