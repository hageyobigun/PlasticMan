using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int hpValue = 1;
    [SerializeField] private int _enemyPos = 4;
    private int[] moveList = { -3, -1, 1, 3 };
    private EnemyMove _enemyMove ;

    protected bool IsDead() => --hpValue <= 0;

    public void Attacked()
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(_enemyPos, gameObject);
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int action = (int)vectorAction[0];
        bool isMove = false;
        switch (action)
        {

            case 0: //Brain内のvalueが1のとき(Wキー)のアクション
                isMove =_enemyMove.IsStage(3);
                break;
            case 1://Brain内のvalueが2のとき(Sキー)のアクション
                isMove = _enemyMove.IsStage(-3);
                break;
            case 2://Brain内のvalueが3のとき(Dキー)のアクション
                isMove = _enemyMove.IsStage(1);
                break;
            case 3://Brain内のvalueが4のとき(Aキー)のアクション
                isMove = _enemyMove.IsStage(-1);
                break;
        }
        if (isMove)
        {
            _enemyMove.Move();
            AddReward(1.0f);
            EndEpisode();
        }
        else
        {
            AddReward(-0.1f);
            EndEpisode();
        }
    }

}
