using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;

public class PlayerAgent : Agent , Player.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;
    [SerializeField] private GameObject enemy = null;
    private int[] moveList = { -3, -1, 1, 3, 0};

    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private BaseEnemyAgent _baseEnemyAgent;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<int> moveSubject = new Subject<int>();
    private Subject<Unit> isAttackSubject = new Subject<Unit>();

    private State playerState;
    private bool isAttack;

    public override void Initialize()
    {
        //_playerMove = new PlayerMove(this.gameObject);
        _playerAttack = GetComponent<PlayerAttack>();
        _baseEnemyAgent = enemy.GetComponent<BaseEnemyAgent>();
        isAttack = true;

        attackSubject
            .Where(attack => attack == 1 && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _playerAttack.BulletAttack();
                playerState = State.Bullet_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 2 && mpValue >= 3 && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
            {
                _playerAttack.FireAttack();
                mpValue -= 3;
                playerState = State.Fire_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 3 && mpValue >= 4 && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(0.7f))
            .Subscribe(_ =>
            {
                _playerAttack.BombAttack();
                mpValue -= 4;
                playerState = State.Bomb_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 4 && mpValue >= 5 && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(2.5f))
            .Subscribe(_ =>
            {
                StartCoroutine(_playerAttack.BarrierGuard());
                mpValue -= 5;
                playerState = State.Barrier;
                isAttack = false;
            });

        isAttackSubject
            .Where(_ => isAttack == false)
            .Delay(TimeSpan.FromSeconds(0.1f))
            .Subscribe(_ => isAttack = true);

        //moveSubject
        //    .Where(move => _playerStage.IsStage(moveList[move]))
        //    .Subscribe(_ => _playerMove.Move(_playerStage.getPlayerPos));

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(enemy.transform.position);
        sensor.AddObservation(mpValue);
    }


    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        playerState = State.Normal;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);
        isAttackSubject.OnNext(Unit.Default);

        if (attack == 0)
        {
            playerState = State.Normal;
        }

        if (_baseEnemyAgent != null)
        {
            if (_baseEnemyAgent.GetHpValue <= 1)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

        if (hpValue < 0)
        {
            EndEpisode();
        }
    }

    public void Attacked(float damage)
    {
        hpValue -= (int)damage;
    }

        //プロパティー
    public int GetHpValue
    {
        get { return this.hpValue; }  //取得用
        private set { this.hpValue = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.playerState; }  //取得用
        private set { this.playerState = value; } //値入力用
    }
}
