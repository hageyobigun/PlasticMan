﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStage
{
    private int _playerPos;

    public PlayerStage(int playerPos)
    {
        _playerPos = playerPos;
    }

    public bool IsStage(int _moveDirection)
    {
        var nextPos = _playerPos + _moveDirection;
        if (nextPos < 0 || nextPos > 8) return false;

        if (_moveDirection == 1 && nextPos % 3 == 0) return false;

        if (_moveDirection == -1 && _playerPos % 3 == 0) return false;

        _playerPos += _moveDirection;

        return true;
    }

    public GameObject getPlayerPos => StageManager.Instance.playerStageList[_playerPos];
}
