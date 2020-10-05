using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    private readonly GameObject _player;
    private int _playerPos = 4;


    public PlayerMove(GameObject gameObject)
    {
        _player = gameObject;
    }

    //改善予定
    public bool IsMove(int _moveDirection)
    {
        var nextPos = _playerPos + _moveDirection;
        if (nextPos < 0 || nextPos > 8) return false;

        if (_moveDirection == 1 && nextPos % 3 == 0) return false;

        if (_moveDirection == -1 && _playerPos % 3 == 0) return false;

        _playerPos += _moveDirection;
        return true;
    }

    public void Move(List<GameObject> playerStageBlock)
    {
        _player.transform.position = playerStageBlock[_playerPos].transform.position;
        _player.transform.position += new Vector3(0, 0, -1);
    }
}
