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
    [SerializeField] private List<GameObject> playerStageBlock = null;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerMove = new PlayerMove(this.gameObject);
        _PlayerAttack = GetComponent<PlayerAttack>();

        //移動
        this.UpdateAsObservable()
            .Where(_ => _playerMove.IsMove(_playerInput.Inputting()))
            .Subscribe(_ => _playerMove.Move(playerStageBlock));

        //攻撃
        this.UpdateAsObservable()
            .Where(_ => _playerInput.IsAttack())
            .Subscribe(_ => _PlayerAttack.BulletAttack());
    }
}
