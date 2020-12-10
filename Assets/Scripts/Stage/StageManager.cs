using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]private GameObject playerStageBlockParent = null;
    [SerializeField]private GameObject enemyStageBlockParent = null;
    private List<GameObject> playerStageList = new List<GameObject>();
    private List<GameObject> enemyStageList = new List<GameObject>();

    public StageCheck _stageCheck;

    private void Start()
    {
        StageLoading();
        _stageCheck = new StageCheck();
    }


    //ステージ読み込み
    private void StageLoading()
    {
        foreach (Transform childTransform in playerStageBlockParent.transform)
        {
            playerStageList.Add(childTransform.gameObject);

        }
        foreach (Transform childTransform in enemyStageBlockParent.transform)
        {
            enemyStageList.Add(childTransform.gameObject);
        }
    }

    public List<GameObject> GetPlayerStageList
    {
        get { return playerStageList;}
    }

    public List<GameObject> GetEnemyStageList
    {
        get { return enemyStageList; }
    }


    //public GameObject GetPlayerStage(int number)
    //{
    //    return (playerStageList[number]);
    //}

    //public GameObject GetEnemyStage(int number)
    //{
    //    return (enemyStageList[number]);
    //}
}
