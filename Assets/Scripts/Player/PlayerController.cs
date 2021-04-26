using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Character;
using Game;
using Mp;

public class PlayerController : MonoBehaviour , IAttackable
{
    private IPlayerInput _playerInput;
    private PlayerMove  _playerMove;
    private AttackManager _attackManager;
    private PlayerAnimation _playerAnimation;
    [SerializeField] private LifePresenter _lifePresenter = null;
    [SerializeField] private ResultPresenter _resultPresenter = default;
    [SerializeField] private StageManager _stageManager = null;
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;

    private State playerAttackState;     //攻撃状態
    private State playerGuardState;    //防御状態

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
            .Where(_ => _playerInput.IsBulltetAttack() && mpValue >= Attack.bulletMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.bulletInterval))
            .Subscribe(_ =>
            {
                _attackManager.BulletAttack();
                playerAttackState = State.Bullet_Attack;
                MpAction(Attack.bulletMp, "Attack");
            });

        //攻撃(fire)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsFireAttack() && mpValue >= Attack.firetMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.firetInterval))
            .Subscribe(_ =>
               {
                   _attackManager.FireAttack();
                   playerAttackState = State.Fire_Attack;
                   MpAction(Attack.firetMp, "Attack");
               }
            );

        //攻撃(bomb)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsBombAttack() && mpValue >= Attack.bombMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.bombInterval))
            .Subscribe(_ =>
                {
                    _attackManager.BombAttack();
                    playerAttackState = State.Bomb_Attack;
                    MpAction(Attack.bombMp, "Attack");
                }
            );

        //防御(barrier)
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)
            .Where(_ => _playerInput.IsBarrier() && mpValue >= Attack.barrierMp)
            .ThrottleFirst(TimeSpan.FromSeconds(AttackInterval.barrierInterval))
            .Subscribe(_ =>
                {
                    BarrierInterVal();
                    playerGuardState = State.Barrier;
                    MpAction(Attack.barrierMp, "Attack");
                }
            );

    }

    //初期化
    private void Initialize()
    {
        _playerInput = new GamePadInput();
        _playerMove = new PlayerMove(this.gameObject, _stageManager.GetStageList(Category.Player));
        _playerAnimation = new PlayerAnimation(GetComponent<Animator>());
        _attackManager = GetComponent<AttackManager>();
        _lifePresenter.Initialize(hpValue, mpValue);
        playerAttackState = State.Normal;
        playerGuardState = State.Normal;
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
                _resultPresenter.Lose();
                Destroy(gameObject);
            }
        }
    }

    //バリアを呼び出し終了後に処理
    private void BarrierInterVal()
    {
        Observable.FromCoroutine(() => _attackManager.BarrierGuard(AttackInterval.barrierInterval))
            .Subscribe(_ => playerGuardState = State.Normal)
            .AddTo(this);
    }

    //攻撃や防御などMP使用行動
    private void MpAction(int useMpvalue, string animationName)
    {
        mpValue -= useMpvalue;
        _lifePresenter.OnChangeMpLife(mpValue);
        _playerAnimation.SetAnimation(animationName);
    }


    //プロパティー
    public int GetHpValue { get { return this.hpValue; }}

    //プロパティー 攻撃状態
    public State GetAttackState{get { return this.playerAttackState; }}

    //プロパティー 防御状態
    public State GetGuardState{get { return this.playerGuardState; }}

}
