using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadInput : IPlayerInput
{
    private float moveX;
    private float moveY;
    private bool isMoveX;
    private bool isMoveY;

    public int Inputting()
    {

        moveX = Input.GetAxis("moveX");
        moveY = Input.GetAxis("moveY");

        //長押しで移動の防止（仮）
        if (moveX == 0)
        {
            isMoveX = true;
        }

        if (moveY == 0)
        {
            isMoveY = true;
        }

        if (isMoveX == true)
        {
            if (moveX > 0)//左
            {
                isMoveX = false;
                return (-1);
            }
            else if (moveX < 0)//右
            {
                isMoveX = false;
                return (1);
            }
        }

        if (isMoveY == true)
        {
            if (moveY > 0)//上
            {
                isMoveY = false;
                return (-3);
            }
            else if (moveY < 0)//下
            {
                isMoveY = false;
                return (3);
            }
        }
        return (0);
    }
    public bool IsBarrier() => Input.GetButtonDown("attack1"); //Aキー  四角ボタン

    public bool IsBombAttack() => Input.GetButtonDown("attack2"); //Wキー  Xボタン

    public bool IsBulltetAttack() => Input.GetButtonDown("attack3"); //Sキー  丸ボタン

    public bool IsFireAttack() => Input.GetButtonDown("attack4"); //Dキー  三角ボタン



}
