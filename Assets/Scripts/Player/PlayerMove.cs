using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    private readonly GameObject player;
    private List<GameObject> stageList = new List<GameObject>(); //ステージのオブジェクトリスト
    private int playerPos;

    public PlayerMove(GameObject _player, List<GameObject> _stageList)
    {
        player = _player;
        stageList = _stageList;
        playerPos = 4; //初期位置
    }

    public bool IsMove(int _moveDirection)
    {
        var nextPos = playerPos + _moveDirection;
        if (nextPos < 0 || nextPos > 8) return false;

        if (_moveDirection == 1 && nextPos % 3 == 0) return false;

        if (_moveDirection == -1 && playerPos % 3 == 0) return false;

        playerPos = nextPos;
        return true;
    }

    public void Move()
    {
        player.transform.position = stageList[playerPos].transform.position;
        player.transform.position += new Vector3(0, -1, -1);
    }
}
