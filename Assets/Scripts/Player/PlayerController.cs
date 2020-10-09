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
    [SerializeField] private GameObject stage = null;


    private void Awake()
    {
        Initialize();

        //移動
        this.UpdateAsObservable()
            .Where(_ => _playerStage.IsStage(_playerInput.Inputting()))
            .Subscribe(_ => _playerMove.Move(_playerStage.PlayerPos));

        //攻撃
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsAttack())
            .Subscribe(_ => _PlayerAttack.BulletAttack());
    }

    private void Initialize()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject);
        _playerStage = new PlayerStage(stage, 4);
        _PlayerAttack = GetComponent<PlayerAttack>();
    }
}
