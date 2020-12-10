using Unity.MLAgents.Sensors;
using UnityEngine;

public class EnduranceEnemyAgent : BaseEnemyAgent
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
