using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    private bool isRushGame = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyList.Count; i++) //最初に全部falseにしておく
        {
            enemyList[i].SetActive(false);
        }

        if (GameManeger.Instance.currentGameStates.Value == GameState.RushGame)
        {
            isRushGame = true;
        }
        enemyList[GameManeger.Instance.GetEnemyNumber].SetActive(true);
    }


}
