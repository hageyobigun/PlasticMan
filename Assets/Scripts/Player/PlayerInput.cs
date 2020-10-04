using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    private float _horizontal;
    private float _vertical;

    public void Inputting()
    {
        _horizontal = 0;
        _vertical = 0;
        if (Input.GetKeyDown(KeyCode.W))
        {
            _vertical = 2;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _vertical = -2;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _horizontal = -3;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _horizontal = 3;
        }
    }

    public Vector3 MoveDirection() => new Vector3(_horizontal, _vertical, 0f);

    public bool IsAttack() => Input.GetKeyDown(KeyCode.Space);
}
