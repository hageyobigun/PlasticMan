using Unity.MLAgents.Sensors;

public class NormalEnemyAgent : BaseEnemyAgent
{

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
        base.OnActionReceived(vectorAction);

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
