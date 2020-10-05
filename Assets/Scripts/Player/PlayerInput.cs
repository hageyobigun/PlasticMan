using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    private int _moveDirection;

    public int Inputting()
    {
        _moveDirection = 0;
        if (Input.GetKeyDown(KeyCode.W))
        {
            _moveDirection = -3;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _moveDirection = 3;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _moveDirection = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _moveDirection = 1;
        }
        return _moveDirection;
    }


    public bool IsAttack() => Input.GetKeyDown(KeyCode.Space);
}
