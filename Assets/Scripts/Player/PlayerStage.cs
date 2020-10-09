using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStage
{
    private GameObject stageBlockParent;
    private List<GameObject> stageList = new List<GameObject>();
    private int _playerPos;

    public PlayerStage(GameObject gameObject, int playerPos)
    {
        stageBlockParent = gameObject;
        _playerPos = playerPos;
        foreach (Transform childTransform in stageBlockParent.transform)
        {
            stageList.Add(childTransform.gameObject);
        }
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

    public GameObject PlayerPos => stageList[_playerPos];
}
