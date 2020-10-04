using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput _playerInput;
    private PlayerMove _playerMove;
    private PlayerAttack _PlayerAttack;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject);
        _PlayerAttack = GetComponent<PlayerAttack>();

        this.UpdateAsObservable().Subscribe(_ => _playerInput.Inputting());

        this.UpdateAsObservable().Subscribe(_ => _playerMove.Move(_playerInput.MoveDirection()));

        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsAttack())
            .Subscribe(_ => _PlayerAttack.BulletAttack());
    }
}
