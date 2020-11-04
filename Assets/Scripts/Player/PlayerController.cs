using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerController : MonoBehaviour , Player.IAttackable
{
    private IPlayerInput _playerInput;
    private PlayerMove  _playerMove;
    private PlayerAttack _PlayerAttack;
    private PlayerStage _playerStage;

    [SerializeField] private int hpValue = 1;
    [SerializeField] private int maxMpValue = 100;
    private int mpValue;

    protected bool IsDead() => --hpValue <= 0;

    private void Awake()
    {
        Initialize();
        mpValue = maxMpValue;

        //移動
        this.UpdateAsObservable()
            .Where(_ => _playerStage.IsStage(_playerInput.Inputting()))
            .Subscribe(_ => _playerMove.Move(_playerStage.getPlayerPos));

        //攻撃(bullet)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBulltetAttack())
            .ThrottleFirst(TimeSpan.FromSeconds(0.3f))
            .Subscribe(_ => _PlayerAttack.BulletAttack());


        //攻撃(fire)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsFireAttack() && mpValue >= 3)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
               {
                   _PlayerAttack.FireAttack();
                   mpValue -= 3;
               }
            );

        //攻撃(bomb)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBombAttack() && mpValue >= 4)
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ =>
                {
                    _PlayerAttack.BombAttack();
                    mpValue -= 4;
                }
            );

        //防御(barrier)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBarrier() && mpValue >= 5)
            .ThrottleFirst(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ =>
                {
                    StartCoroutine(_PlayerAttack.BarrierGuard());
                    mpValue -= 5;
                }
            );

        //mp回復
        Observable.Interval(TimeSpan.FromSeconds(1.0))
            .Where(_ => mpValue <= maxMpValue)
            .Subscribe(_ => mpValue++)
            .AddTo(gameObject);
    }

    private void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(4);
        _PlayerAttack = GetComponent<PlayerAttack>();
    }

    public void Attacked(float damage)
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

}
