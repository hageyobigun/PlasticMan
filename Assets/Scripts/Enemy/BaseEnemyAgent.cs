using UnityEngine;
using Unity.MLAgents;
using UniRx;
using System;
using Character;
using Game;

public abstract class BaseEnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int MaxHpValue = 100;
    [SerializeField] private int MaxMpValue = 100;
    private int hpValue;
    private int mpValue;
    private State enemyState;
    private EnemyMove _enemyMove;
    protected EnemyAttacks _enemyAttacks;
    [SerializeField] private LifePresenter _lifePresenter = null;
    [SerializeField] private StageManager _stageManager = null;

    private Subject<int> moveSubject = new Subject<int>();
    private Subject<int> attackSubject = new Subject<int>();
    public IObservable<int> attackObservable { get { return attackSubject; }}

    public GameObject player;
    protected PlayerAgent _playerAgent;
    protected PlayerController _playerController;

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(gameObject, _stageManager.GetEnemyStageList);
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _playerAgent = player.GetComponent<PlayerAgent>();
        _playerController = player.GetComponent<PlayerController>();

        moveSubject
            .Where(move => _enemyMove.IsMove(move))
            .Subscribe(_ => _enemyMove.Move());

    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _lifePresenter.Initialize(MaxHpValue, MaxMpValue);
        hpValue = MaxHpValue;
        mpValue = MaxMpValue;
        enemyState = State.Normal;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0]; //1:上　2:左　3:右　4:下　5:静止
        int attack = (int)vectorAction[1];

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);
        if (attack == 0)
        {
            enemyState = State.Normal;
        }

        if (GetHpValue <= 0)//死亡
        {
            EndEpisode();
        }

        if (player != null)
        {
            if (_playerController != null)//切り替え可能
            {
                if (_playerController.GetHpValue <= 0) //撃破
                {
                    AddReward(1.0f);
                    EndEpisode();
                }
            }
        }
    }

    public virtual void Attacked(float damage)
    {
        hpValue -= (int)damage;
        _lifePresenter.OnChangeHpLife(hpValue);
        if (_playerController != null && hpValue <= 0)
        {
            GameManeger.Instance.currentGameStates.Value = GameState.Win;
            Destroy(gameObject);
        }
    }

    //MP消費
    public void MpConsumption(int useValue)
    {
        mpValue -= useValue;
        _lifePresenter.OnChangeMpLife(mpValue);
    }

    //プロパティー
    public int GetHpValue
    {
        get { return this.hpValue; }  //取得用
        set { this.hpValue = value; } //値入力用
    }

    //プロパティー
    public int GetMpValue
    {
        get { return this.mpValue; }  //取得用
        set { this.mpValue = value; } //値入力用
    }

    //プロパティー
    public State GetState
    {
        get { return this.enemyState; }  //取得用
        set { this.enemyState = value; } //値入力用
    }
}
