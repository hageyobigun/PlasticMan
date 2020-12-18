using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyList.Count; i++) //最初に全部falseにしておく
        {
            enemyList[i].SetActive(false);
        }
        if (GameManeger.Instance.GetEnemyNumber < enemyList.Count)
        {
            enemyList[GameManeger.Instance.GetEnemyNumber].SetActive(true);//戦う敵を出現させる
        }
        else
        {
            if (GameManeger.Instance.currentGameStates.Value == GameState.RushGame) //BossRush Clear
            {
                GameManeger.Instance.currentGameStates.Value = GameState.RushClear;
            }
            else
            {
                Debug.LogError("enemy_not_found"); //敵が登録されていない場合
            }
        }
    }
}
