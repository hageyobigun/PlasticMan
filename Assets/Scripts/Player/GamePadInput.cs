using UnityEngine;

public class GamePadInput : IPlayerInput
{
    private float moveX;
    private float moveY;
    private bool isMoveX;
    private bool isMoveY;

    //ps4コントローラーとキーボード
    //移動
    public int Inputting()
    {
        moveX = Input.GetAxis("moveX");
        moveY = Input.GetAxis("moveY");

        //長押しで移動の防止
        if (moveX == 0) 
        {
            isMoveX = true;
        }
        if (moveY == 0)
        {
            isMoveY = true;
        }

        //-1, 1 動く方向 左、右
        if (isMoveX == true && moveX != 0)
        {
            isMoveX = false;
            return ((int)moveX * -1);
        }

        //-3, 3 動く方向 上、下
        if (isMoveY == true && moveY != 0)
        {
            isMoveY = false;
            return ((int)moveY * -3);
        }
        return (0);
    }

    public bool IsBarrier() => Input.GetButtonDown("attack1"); //Aキー  四角ボタン

    public bool IsBombAttack() => Input.GetButtonDown("attack2"); //Wキー  Xボタン

    public bool IsBulltetAttack() => Input.GetButtonDown("attack3"); //Sキー  丸ボタン

    public bool IsFireAttack() => Input.GetButtonDown("attack4"); //Dキー  三角ボタン



}
