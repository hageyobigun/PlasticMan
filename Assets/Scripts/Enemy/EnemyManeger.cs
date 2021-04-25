using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private ResultPresenter _resultPresenter = default;

    // Start is called before the first frame update
    void Start()
    {
        var enemyNumber = GameManeger.Instance.EnemyNumber;
        for (int i = 0; i < enemyList.Count; i++) //最初に全部falseにしておく
        {
            enemyList[i].SetActive(false);
        }

        if (enemyNumber < enemyList.Count)
        {
            enemyList[enemyNumber].SetActive(true);//戦う敵を出現させる
        }
        else
        {
            Debug.LogError("No enemy found"); //敵が登録されていない場合
        }
        if (enemyNumber == enemyList.Count - 1) //ラストの敵なのでrushのクリアのフラグ
        {
            _resultPresenter.IsRushClear();
        }
    }

}
