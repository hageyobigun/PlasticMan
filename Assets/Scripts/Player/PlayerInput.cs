using UnityEngine;

public class PlayerInput : IPlayerInput
{

    public int Inputting()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))//上
        {
             return (-3);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//下
        {
            return (3);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))//左
        {
            return (-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))//右
        {
            return (1);
        }
        return (0);
    }

    public bool IsBulltetAttack() => Input.GetKeyDown(KeyCode.Space);

    public bool IsFireAttack() => Input.GetKeyDown(KeyCode.V);

    public bool IsBombAttack() => Input.GetKeyDown(KeyCode.B);

    public bool IsBarrier() => Input.GetKeyDown(KeyCode.C);

}
