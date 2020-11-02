using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent , Player.IAttackable
{
    [SerializeField] private int hpValue = 1;
    [SerializeField] private GameObject enemy = null;

    private int[] moveList = { -3, -1, 1, 3 };

    private PlayerMove _playerMove;
    private PlayerStage _playerStage;
    private PlayerAttack _PlayerAttack;

    private EnemyAgent _enemyAgent;



    protected bool IsDead() => --hpValue <= 0;

    //プロパティー
    public int GetHpValue
    {
        get { return this.hpValue; }  //取得用
        private set { this.hpValue = value; } //値入力用
    }

    public void Attacked(float damage)
    {
        IsDead();
    }

    public override void Initialize()
    {
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(4);
        _PlayerAttack = GetComponent<PlayerAttack>();
        _enemyAgent = enemy.GetComponent<EnemyAgent>();
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        hpValue = 5;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(enemy.transform.position);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //StartCoroutine(Move(vectorAction));
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        bool isMove = _playerStage.IsStage(moveList[move]);
        if (attack == 1) _PlayerAttack.BulletAttack();

        if (_enemyAgent != null)
        {
            if (_enemyAgent.GetHpValue <= 0)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }
        if (isMove)
        {
            _playerMove.Move(_playerStage.getPlayerPos);
        }
        if (hpValue <= 0)
        {
            EndEpisode();
        }
    }

    //IEnumerator Move(float[] vectorAction)
    //{
    //    int move = (int)vectorAction[0];
    //    int attack = (int)vectorAction[1];

    //    yield return new WaitForSeconds(1.0f);

    //    bool isMove = _playerStage.IsStage(moveList[move]);
    //    if (attack == 1) _PlayerAttack.BulletAttack();

    //    if (isMove)
    //    {
    //        _playerMove.Move(_playerStage.getPlayerPos);
    //        AddReward(0.5f);
    //    }
    //    else
    //    {
    //        AddReward(-0.1f);
    //    }

    //    if (hpValue < 0)
    //    {
    //        AddReward(-0.5f);
    //        EndEpisode();
    //    }
    //}
}
