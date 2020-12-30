using UnityEngine;
using Game;

public class ResultButton : MonoBehaviour
{
    //BossRush Button

    public void NextEnemyButton() //次の的に進む
    {
        GameManeger.Instance.GetEnemyNumber++;
        GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
    }

    public void RetryRushButton()
    {
        GameManeger.Instance.GetEnemyNumber = 0;
        GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
    }


    //Vs Button

    public void RetryVsButton()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }


    //共通リトライボタン

    public void BackOptionButton()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.Start;
    }

    public void TitleButton()
    {
        GameManeger.Instance.currentGameStates.Value = GameState.Title;
    }

}
