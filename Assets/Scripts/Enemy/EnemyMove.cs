using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove
{
    private readonly GameObject enemy;
    private List<GameObject> stageList = new List<GameObject>(); //ステージのオブジェクトリスト
    private int enemyPos;
    private int[] moveList = {0, -3, -1, 1, 3};//動く方向のリスト{静止, 上、左、右、下}

    public EnemyMove(GameObject _enemy, List<GameObject> _stageList)
    {
        enemy = _enemy;
        stageList = _stageList;
        enemyPos = 4;//初期位置
    }

    public bool IsMove(int moveDirection)
    {
        moveDirection = moveList[moveDirection];//動く方向の値に修正

        var nextPos = enemyPos + moveDirection;
        if (nextPos < 0 || nextPos > 8) return false;

        if (moveDirection == 1 && nextPos % 3 == 0) return false;

        if (moveDirection == -1 && enemyPos % 3 == 0) return false;

        enemyPos += moveDirection;
        return true;
    }

    public void Move()
    {
        enemy.transform.position = stageList[enemyPos].transform.position;
        enemy.transform.position += new Vector3(0, -1, -1);
    }

}
