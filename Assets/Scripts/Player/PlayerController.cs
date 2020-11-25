using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Character;

public class PlayerController : MonoBehaviour , Player.IAttackable
{
    private IPlayerInput _playerInput;
    private PlayerMove  _playerMove;
    private PlayerAttack _PlayerAttack;
    private PlayerStage _playerStage;
    private SliderModel _sliderModel;

    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;

    protected bool IsDead() => _sliderModel.hp.Value <= 0;
    private State playerState;

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

    private void Awake()
    {
        Initialize();

        //移動
        this.UpdateAsObservable()
            .Where(_ => _playerStage.IsStage(_playerInput.Inputting()))
            .Subscribe(_ => _playerMove.Move(_playerStage.getPlayerPos));

        //攻撃(bullet)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBulltetAttack())
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ =>
            {
                _PlayerAttack.BulletAttack();
                playerState = State.Bullet_Attack;
            });


        //攻撃(fire)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsFireAttack() && _sliderModel.mp.Value >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
               {
                   _PlayerAttack.FireAttack();
                   _sliderModel.mp.Value -= 3;
                   playerState = State.Fire_Attack;
               }
            );

        //攻撃(bomb)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBombAttack() && _sliderModel.mp.Value >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
                {
                    _PlayerAttack.BombAttack();
                    _sliderModel.mp.Value -= 4;
                    playerState = State.Bomb_Attack;
                }
            );

        //防御(barrier)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBarrier() && _sliderModel.mp.Value >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ =>
                {
                    StartCoroutine(_PlayerAttack.BarrierGuard());
                    _sliderModel.mp.Value -= 5;
                    playerState = State.Barrier;
                }
            );
    }

    private void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(4);
        _PlayerAttack = GetComponent<PlayerAttack>();
        _sliderModel = GetComponent<SliderModel>();
        _sliderModel.Initialize(hpValue, mpValue);
        playerState = State.Normal;
    }

    public void Attacked(float damage)
    {
        _sliderModel.hp.Value -= (int)damage;

        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

}
