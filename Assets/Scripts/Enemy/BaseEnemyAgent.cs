using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;

public abstract class BaseEnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;
    [SerializeField] private GameObject player = null;
    private int[] moveList = { -3, -1, 1, 3, 0};

    private EnemyMove _enemyMove;
    private SliderModel _sliderModel;

    private Subject<int> attackSubject = new Subject<int>();
    public IObservable<int> attackObservable { get { return attackSubject; }}

    private Subject<int> moveSubject = new Subject<int>();
    private State enemyState;

    protected PlayerAgent _playerAgent;
    protected PlayerController _playerController;

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(4, gameObject);
        _playerAgent = player.GetComponent<PlayerAgent>();
        _playerController = player.GetComponent<PlayerController>();
        _sliderModel = GetComponent<SliderModel>();

        moveSubject
            .Where(move => _enemyMove.IsStage(moveList[move]))
            .Subscribe(_ => _enemyMove.Move());
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _sliderModel.Initialize(hpValue, mpValue);
        enemyState = State.Normal;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(player.transform.position);
        sensor.AddObservation(_sliderModel.hp.Value);
        sensor.AddObservation(_sliderModel.mp.Value);
        sensor.AddObservation((float)enemyState);
        if (_playerAgent != null) sensor.AddObservation((float)_playerAgent.GetState);
        else if (_playerController != null) sensor.AddObservation((float)_playerController.GetState);

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);
    }

    public virtual void Attacked(float damage)
    {
        _sliderModel.hp.Value -= (int)damage;
    }

    //プロパティー
    public int GetHpValue
    {
        get { return this._sliderModel.hp.Value; }  //取得用
        set { this._sliderModel.hp.Value = value; } //値入力用
    }

    //プロパティー
    public int GetMpValue
    {
        get { return this._sliderModel.mp.Value; }  //取得用
        set { this._sliderModel.mp.Value = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.enemyState; }  //取得用
        set { this.enemyState = value; } //値入力用
    }
}
