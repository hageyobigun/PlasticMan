using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game;

public class VsButton : MonoBehaviour
{
    [SerializeField] private List<Button> enemyButtons = new List<Button>();

    void Awake()
    {
        for(int buttonNUmber = 0; buttonNUmber < enemyButtons.Count; buttonNUmber++)
        {
            SetButton(buttonNUmber);
        }
    }

    private void SetButton(int number)
    {
        enemyButtons[number].OnClickAsObservable()
            .Subscribe(_ => SelectEnemy(number));
    }

    private void SelectEnemy(int enemyNUmber)
    {
        GameManeger.Instance.GetEnemyNumber = enemyNUmber;
        GameManeger.Instance.currentGameStates.Value = GameState.VsGame;
    }

}
