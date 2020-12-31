using UnityEngine;
using Game;

public class ResultButton : MonoBehaviour
{
    //BossRush Button

    public void NextEnemyButton() //次の的に進む
    {
        SoundManager.Instance.PlaySe("NormalButton");
        GameManeger.Instance.GetEnemyNumber++;
        GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
    }

    public void RetryRushButton()
    {
        SoundManager.Instance.PlaySe("NormalButton");
        GameManeger.Instance.GetEnemyNumber = 0;
        GameManeger.Instance.currentGameStates.Value = GameState.RushGame;
    }


    //Vs Button

    public void RetryVsButton()
    {
        SoundManager.Instance.PlaySe("NormalButton");
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }


    //共通リトライボタン

    public void BackOptionButton()
    {
        SoundManager.Instance.PlaySe("NormalButton");
        GameManeger.Instance.currentGameStates.Value = GameState.Start;
    }

    public void TitleButton()
    {
        SoundManager.Instance.PlaySe("NormalButton");
        GameManeger.Instance.currentGameStates.Value = GameState.Title;
    }

}
