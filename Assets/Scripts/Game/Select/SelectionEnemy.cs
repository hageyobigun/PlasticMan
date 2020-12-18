using UnityEngine;
using Game;

public class SelectionEnemy : MonoBehaviour
{

    public void Enemy_One()
    {
        GameManeger.Instance.GetEnemyNumber = 0;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

    public void Enemy_Two()
    {
        GameManeger.Instance.GetEnemyNumber = 1;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

    public void Enemy_Three()
    {
        GameManeger.Instance.GetEnemyNumber = 2;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

    public void Enemy_Rush()
    {
        GameManeger.Instance.GetEnemyNumber = 0;
        GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
    }
}
