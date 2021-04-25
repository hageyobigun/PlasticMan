using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UniRx;
using System;
using Character;
using Mp;

//enemyの学習のための対戦相手の役割 行動基準は基本player
public class PlayerAgent : Agent , IAttackable
{
    [SerializeField] private int maxHpValue = 100;
    [SerializeField] private int maxMpValue = 100;
    private int hpValue;
    private int mpValue;

    [SerializeField] private GameObject enemy = null;
    private int[] moveList = { 0, -3, -1, 1, 3 };//動く方向のリスト{静止, 上、左、右、下}

    private PlayerMove _playerMove;
    private AttackManager _attackManager;
    [SerializeField] private LifePresenter _lifePresenter = null;
    [SerializeField] private StageManager _stageManager = null;
    private BaseEnemyAgent _baseEnemyAgent;

    private Subject<int> attackSubject = new Subject<int>();
    private Subject<int> moveSubject = new Subject<int>();

    private State playerAttackState;
    private State playerGuardState;

    private bool isAttack;

    public override void Initialize()
    {
        _playerMove = new PlayerMove(this.gameObject, _stageManager.GetStageList(Category.Player));
        _attackManager = GetComponent<AttackManager>();
        _baseEnemyAgent = enemy.GetComponent<BaseEnemyAgent>();
        isAttack = true;

        attackSubject
            .Where(attack => attack == 0)
            .Subscribe(_ => playerAttackState = State.Normal);//攻撃しない

        attackSubject
            .Where(attack => attack == 1 && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.bulletInterval))
            .Subscribe(_ =>
            {
                _attackManager.BulletAttack();
                playerAttackState = State.Bullet_Attack;
            });

        attackSubject
            .Where(attack => attack == 2 && mpValue >= Attack.firetMp && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.firetInterval))
            .Subscribe(_ =>
            {
                _attackManager.FireAttack();
                MpConsumption(Attack.firetMp);
                playerAttackState = State.Fire_Attack;
            });

        attackSubject
            .Where(attack => attack == 3 && mpValue >= Attack.bombMp && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.bombInterval))
            .Subscribe(_ =>
            {
                _attackManager.BombAttack();
                MpConsumption(Attack.bombMp);
                playerAttackState = State.Bomb_Attack;
            });

        attackSubject
            .Where(attack => attack == 4 && mpValue >= Attack.barrierMp && isAttack == true)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.barrierInterval))
            .Subscribe(_ =>
            {
                BarrierInterVal();
                MpConsumption(Attack.barrierMp);
                playerGuardState = State.Barrier;
            });

        moveSubject
            .Where(move => _playerMove.IsMove(moveList[move]))
            .Subscribe(_ => _playerMove.Move());

    }

    //観察対象 個数:9(自分の位置(3),enemyの位置(3) 自分のMP(1),　自分の攻撃状態(1),自分の防御状態(1))
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(enemy.transform.position);
        sensor.AddObservation(mpValue);
        sensor.AddObservation((float)playerAttackState);
        sensor.AddObservation((float)playerGuardState);
    }


    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        _lifePresenter.Initialize(maxHpValue, maxMpValue);
        hpValue = maxHpValue;
        mpValue = maxMpValue;
        playerAttackState = State.Normal;
        playerGuardState = State.Normal;
    }

    //行動
    public override void OnActionReceived(float[] vectorAction)
    {
        int move = (int)vectorAction[0]; //0:静止 1:上　2:左　3:右　4:下
        int attack = (int)vectorAction[1]; //攻撃　0:なし　1:通常弾　2:炎 3:ボム 4:バリア

        moveSubject.OnNext(move);
        attackSubject.OnNext(attack);

        if (_baseEnemyAgent != null)
        {
            if (_baseEnemyAgent.HpValue <= 1)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }

        //if (StepCount % 1000 == 0)
        //{
        //    AddReward(0.1f);
        //}
    }

    //ダメージ処理
    public void Attacked(float damage)
    {
        if (playerGuardState != State.Barrier)
        {
            hpValue -= (int)damage;
            _lifePresenter.OnChangeHpLife(hpValue);
            if (hpValue < 0)//死亡
            {
                AddReward(-0.1f);
                EndEpisode();
            }
        }
    }

    ////死亡した時、向こうがEndEpisodeをする前に終了してしまう対策
    //public IEnumerator EndPlayer()
    //{
    //    yield return new WaitForSeconds(0.0f);
    //    AddReward(-0.1f);
    //    EndEpisode();

    //}

    //バリアを呼び出し終了後に処理
    public void BarrierInterVal()
    {
        Observable.FromCoroutine(() => _attackManager.BarrierGuard(AttackInterval.bulletInterval))
            .Subscribe(_ => playerGuardState = State.Normal)
            .AddTo(this);
    }

    //MP消費
    private void MpConsumption(int useValue)
    {
        mpValue -= useValue;
        _lifePresenter.OnChangeMpLife(mpValue);
    }

    //プロパティー
    public int GetHpValue { get { return this.hpValue; } }

    //プロパティー 攻撃状態
    public State GetAttackState { get { return this.playerAttackState; } }

    //プロパティー 防御状態
    public State GetGuardState { get { return this.playerGuardState; } }

}
