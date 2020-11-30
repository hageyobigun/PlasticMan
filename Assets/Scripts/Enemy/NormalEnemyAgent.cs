using Unity.MLAgents.Sensors;

public class NormalEnemyAgent : BaseEnemyAgent
{

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation((float)GetState);
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
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
