using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;


public class PlayerAgentTest : Agent, Player.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;

    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<Unit> isAttackSubject = new Subject<Unit>();
    private bool isAttack;

    private State playerState;
    private int playerPos;

    public override void Initialize()
    {
        //_playerMove = new PlayerMove(this.gameObject);
        _playerAttack = GetComponent<PlayerAttack>();

        attackSubject
            .Where(attack => attack == 1 && isAttack == true)
            .Subscribe(_ =>
            {
                _playerAttack.BulletAttack();
                playerState = State.Bullet_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 2 && hpValue >= 3 && isAttack == true)
            .Subscribe(_ =>
            {
                _playerAttack.FireAttack();
                mpValue -= 3;
                playerState = State.Fire_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 3 && mpValue >= 4 && isAttack == true)
            .Subscribe(_ =>
            {
                _playerAttack.BombAttack();
                mpValue -= 4;
                playerState = State.Bomb_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 4 && mpValue >= 5 && isAttack == true)
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

    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        playerState = State.Normal;
        playerPos = UnityEngine.Random.Range(0, 9);
        //_playerMove.SelectMove(playerPos);
        isAttack = true;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(hpValue);
        sensor.AddObservation(mpValue);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int attack = (int)vectorAction[0];
        attackSubject.OnNext(attack);
        isAttackSubject.OnNext(Unit.Default);
        if (StepCount % 5000 == 0)
        {
            playerPos = UnityEngine.Random.Range(0, 9);
            //_playerMove.SelectMove(playerPos);
        }
        if (hpValue <= 0)
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
