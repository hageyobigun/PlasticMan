using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetActive(false);
        }
        enemyList[GameManeger.Instance.enemyNumber].SetActive(true);
    }


}
