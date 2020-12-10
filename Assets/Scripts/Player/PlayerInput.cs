using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    private int moveDirection;

    public int Inputting()
    {
        moveDirection = 0;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//上
        {
            moveDirection = -3;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//下
        {
            moveDirection = 3;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))//左
        {
            moveDirection = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))//右
        {
            moveDirection = 1;
        }
        return moveDirection;
    }

    public bool IsBulltetAttack() => Input.GetKeyDown(KeyCode.Space);

    public bool IsFireAttack() => Input.GetKeyDown(KeyCode.V);

    public bool IsBombAttack() => Input.GetKeyDown(KeyCode.B);

    public bool IsBarrier() => Input.GetKeyDown(KeyCode.C);

}
