using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    private int _moveDirection;

    public int Inputting()
    {
        _moveDirection = 0;
        if (Input.GetKeyDown(KeyCode.W))//上
        {
            _moveDirection = -3;
        }
        else if (Input.GetKeyDown(KeyCode.S))//下
        {
            _moveDirection = 3;
        }
        else if(Input.GetKeyDown(KeyCode.A))//右
        {
            _moveDirection = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))//左
        {
            _moveDirection = 1;
        }
        return _moveDirection;
    }

    public bool IsBulltetAttack() => Input.GetKeyDown(KeyCode.Space);

    public bool IsFireAttack() => Input.GetKeyDown(KeyCode.V);

    public bool IsBombAttack() => Input.GetKeyDown(KeyCode.B);

}
