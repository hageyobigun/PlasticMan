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
    [SerializeField] private LifePresenter _lifePresente = null;
    [SerializeField] private StageManager _stageManager = null;
    [SerializeField] private int hpValue = 100;
    [SerializeField] private int mpValue = 100;

    private State playerState;


    private void Awake()
    {
        Initialize();

        //移動
        this.UpdateAsObservable()
            .Where(_ => _playerMove.IsMove(_playerInput.Inputting()))
            .Subscribe(_ => _playerMove.Move());

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
            .Where(_ => _playerInput.IsFireAttack() && mpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
               {
                   _PlayerAttack.FireAttack();
                   MpConsumption(3);
                   playerState = State.Fire_Attack;
               }
            );

        //攻撃(bomb)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBombAttack() && mpValue >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
                {
                    _PlayerAttack.BombAttack();
                    MpConsumption(4);
                    playerState = State.Bomb_Attack;
                }
            );

        //防御(barrier)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBarrier() && mpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ =>
                {
                    StartCoroutine(_PlayerAttack.BarrierGuard());
                    MpConsumption(5);
                    playerState = State.Barrier;
                }
            );
    }

    //初期化
    private void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject, _stageManager.GetPlayerStageList);
        _PlayerAttack = GetComponent<PlayerAttack>();
        _lifePresente.Initialize(hpValue, mpValue);
        playerState = State.Normal;
    }

    //ダメージを受ける
    public void Attacked(float damage)
    {
        hpValue -= (int)damage;
        _lifePresente.OnChangeHpLife(hpValue);
        if (hpValue <= 0)
        {
            GameManeger.Instance.SetCurrentState(GameManeger.GameState.Lose);
            Destroy(gameObject);
        }
    }

    //MP消費
    private void MpConsumption(int useValue)
    {
        mpValue -= useValue;
        _lifePresente.OnChangeMpLife(mpValue);
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
