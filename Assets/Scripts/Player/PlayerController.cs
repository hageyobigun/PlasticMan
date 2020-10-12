using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput _playerInput;
    private PlayerMove  _playerMove;
    private PlayerAttack _PlayerAttack;
    private PlayerStage _playerStage;

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
            .Subscribe(_ => _PlayerAttack.BulletAttack());

        //攻撃(fire)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsFireAttack())
            .Subscribe(_ => _PlayerAttack.FireAttack());

        //攻撃(bomb)
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsBombAttack())
            .Subscribe(_ => _PlayerAttack.BombAttack());
    }

    private void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(4);
        _PlayerAttack = GetComponent<PlayerAttack>();
    }
}
