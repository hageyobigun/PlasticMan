using UnityEngine;

public class PlayerInput : IPlayerInput
{
    //キーボードのみ
    public int Inputting()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))//上
        {
             return (-3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))//下
        {
            return (3);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))//左
        {
            return (-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))//右
        {
            return (1);
        }
        return (0);
    }

    public bool IsBulltetAttack() => Input.GetKeyDown(KeyCode.S);

    public bool IsFireAttack() => Input.GetKeyDown(KeyCode.D);

    public bool IsBombAttack() => Input.GetKeyDown(KeyCode.W);

    public bool IsBarrier() => Input.GetKeyDown(KeyCode.A);

}
