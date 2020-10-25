using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove
{
    private int _enemyPos;
    private GameObject enemy;

    public EnemyMove(int enemyPos, GameObject gameObject)
    {
        _enemyPos = enemyPos;
        enemy = gameObject;
    }

    public bool IsStage(int _moveDirection)
    {
        var nextPos = _enemyPos + _moveDirection;
        if (nextPos < 0 || nextPos > 8) return false;

        if (_moveDirection == 1 && nextPos % 3 == 0) return false;

        if (_moveDirection == -1 && _enemyPos % 3 == 0) return false;

        _enemyPos += _moveDirection;

        return true;
    }

    public void Move()
    {
        enemy.transform.position = StageManager.Instance.enemyStageList[_enemyPos].transform.position;
        enemy.transform.position += new Vector3(0, -1, -1);
    }
}
