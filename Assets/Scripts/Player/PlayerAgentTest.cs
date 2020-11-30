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
    private SliderModel _sliderModel;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<Unit> isAttackSubject = new Subject<Unit>();
    private bool isAttack;

    private State playerState;
    private int playerPos;

    public override void Initialize()
    {
        _playerMove = new PlayerMove(this.gameObject);
        _playerAttack = GetComponent<PlayerAttack>();
        _sliderModel = GetComponent<SliderModel>();

        attackSubject
            .Where(attack => attack == 1 && isAttack == true)
            .Subscribe(_ =>
            {
                _playerAttack.BulletAttack();
                playerState = State.Bullet_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 2 && _sliderModel.mp.Value >= 3 && isAttack == true)
            .Subscribe(_ =>
            {
                _playerAttack.FireAttack();
                _sliderModel.mp.Value -= 3;
                playerState = State.Fire_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 3 && _sliderModel.mp.Value >= 4 && isAttack == true)
            .Subscribe(_ =>
            {
                _playerAttack.BombAttack();
                _sliderModel.mp.Value -= 4;
                playerState = State.Bomb_Attack;
                isAttack = false;
            });

        attackSubject
            .Where(attack => attack == 4 && _sliderModel.mp.Value >= 5 && isAttack == true)
            .Subscribe(_ =>
            {
                StartCoroutine(_playerAttack.BarrierGuard());
                _sliderModel.mp.Value -= 5;
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
        _sliderModel.Initialize(hpValue, mpValue);
        playerState = State.Normal;
        playerPos = UnityEngine.Random.Range(0, 9);
        _playerMove.SelectMove(playerPos);
        isAttack = true;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(_sliderModel.hp.Value);
        sensor.AddObservation(_sliderModel.mp.Value);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int attack = (int)vectorAction[0];
        attackSubject.OnNext(attack);
        isAttackSubject.OnNext(Unit.Default);
        if (StepCount % 5000 == 0)
        {
            playerPos = UnityEngine.Random.Range(0, 9);
            _playerMove.SelectMove(playerPos);
        }
        if (_sliderModel.hp.Value <= 0)
        {
            EndEpisode();
        }
    }

    public void Attacked(float damage)
    {
        _sliderModel.hp.Value -= (int)damage;
    }

    //プロパティー
    public int GetHpValue
    {
        get { return this._sliderModel.hp.Value; }  //取得用
        private set { this._sliderModel.hp.Value = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.playerState; }  //取得用
        private set { this.playerState = value; } //値入力用
    }
}
