using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Character;
using Game;

public class PlayerController : MonoBehaviour , Player.IAttackable
{
    private IPlayerInput _playerInput;
    private PlayerMove  _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerAnimation _playerAnimation;
    [SerializeField] private LifePresenter _lifePresenter = null;
    [SerializeField] private StageManager _stageManager = null;
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;

    private State playerAttackState;     //攻撃状態
    private State playerGuardState;    //防御状態
    [SerializeField] private float barrierInterval = 1.0f;
    
    private void Awake()
    {
        Initialize();

        //移動(値が変化した時(動いた時だけ）
        this.ObserveEveryValueChanged(value => _playerInput.Inputting())
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(value => value != 0)
            .Where(value => _playerMove.IsMove(value))
            .Subscribe(_ => _playerMove.Move());


        //攻撃(bullet)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsBulltetAttack())
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _playerAttack.BulletAttack();
                playerAttackState = State.Bullet_Attack;
                _playerAnimation.SetAnimation("Attack");
            });


        //攻撃(fire)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsFireAttack() && mpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
               {
                   _playerAttack.FireAttack();
                   MpConsumption(3);
                   _playerAnimation.SetAnimation("Attack");
                   playerAttackState = State.Fire_Attack;
               }
            );

        //攻撃(bomb)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsBombAttack() && mpValue >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
                {
                    _playerAttack.BombAttack();
                    MpConsumption(4);
                    _playerAnimation.SetAnimation("Attack");
                    playerAttackState = State.Bomb_Attack;
                }
            );

        //防御(barrier)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsBarrier() && mpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(barrierInterval))
            .Subscribe(_ =>
                {
                    BarrierInterVal();
                    MpConsumption(5);
                    playerGuardState = State.Barrier;
                }
            );

    }

    //初期化
    private void Initialize()
    {
        _playerInput = new GamePadInput();
        _playerMove = new PlayerMove(this.gameObject, _stageManager.GetPlayerStageList);
        _playerAttack = GetComponent<PlayerAttack>();
        _lifePresenter.Initialize(hpValue, mpValue);
        playerAttackState = State.Normal;
        playerGuardState = State.Normal;
        _playerAnimation = new PlayerAnimation(GetComponent<Animator>());
    }

    //ダメージを受ける
    public void Attacked(float damage)
    {
        if (playerGuardState != State.Barrier) //バリア状態だったら無効化
        {
            hpValue -= (int)damage;
            _lifePresenter.OnChangeHpLife(hpValue);
            _playerAnimation.SetAnimation("Damage");
            if (hpValue <= 0)
            {
                ResultManeger.Instance.Lose();
                Destroy(gameObject);
            }
        }
    }

    //バリアを呼び出し終了後に処理
    public void BarrierInterVal()
    {
        Observable.FromCoroutine(() => _playerAttack.BarrierGuard(barrierInterval))
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
    public int GetHpValue { get { return this.hpValue; }}

    //プロパティー 攻撃状態
    public State GetAttackState{get { return this.playerAttackState; }}

    //プロパティー 防御状態
    public State GetGuardState{get { return this.playerGuardState; }}

}
