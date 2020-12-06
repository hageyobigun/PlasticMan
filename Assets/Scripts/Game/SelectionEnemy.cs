using UnityEngine;

public class SelectionEnemy : MonoBehaviour
{

    public void Enemy_One()
    {
        GameManeger.Instance.enemyNumber = 0;
        GameManeger.Instance.SetCurrentState(GameManeger.GameState.Playing_Vs);
    }

    public void Enemy_Two()
    {
        GameManeger.Instance.enemyNumber = 1;
        GameManeger.Instance.SetCurrentState(GameManeger.GameState.Playing_Vs);
    }

    public void Enemy_Three()
    {
        GameManeger.Instance.enemyNumber = 2;
        GameManeger.Instance.SetCurrentState(GameManeger.GameState.Playing_Vs);
    }
}
