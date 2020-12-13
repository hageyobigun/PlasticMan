using UnityEngine;
using Game;

public class SelectionEnemy : MonoBehaviour
{

    public void Enemy_One()
    {
        GameManeger.Instance.enemyNumber = 0;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

    public void Enemy_Two()
    {
        GameManeger.Instance.enemyNumber = 1;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

    public void Enemy_Three()
    {
        GameManeger.Instance.enemyNumber = 2;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }
}
