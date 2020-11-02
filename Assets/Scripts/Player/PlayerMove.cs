using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    private readonly GameObject _player;

    public PlayerMove(GameObject gameObject)
    {
        _player = gameObject;
    }

    public void Move(GameObject movePos)
    {
        _player.transform.position = movePos.transform.position;
        _player.transform.position += new Vector3(0, -1, -1);
    }

}
