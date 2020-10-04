using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    private readonly GameObject _gameObject;

    public PlayerMove(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void Move(Vector3 moveDirection)
    {
        _gameObject.transform.Translate(moveDirection);
    }
}
