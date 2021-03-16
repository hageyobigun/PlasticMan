using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    [SerializeField]private GameObject playerStageBlockParent = null;
    [SerializeField]private GameObject enemyStageBlockParent = null;
    private List<GameObject> playerStageList = new List<GameObject>();
    private List<GameObject> enemyStageList = new List<GameObject>();

    private void Start()
    {
        StageLoading();
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

    //どの台に乗っているか
    public int GetPlayerPosNumber(Vector3 checkPos)
    {
        var number = 0;
        for(number = 0; number < playerStageList.Count; number++)
        {
            if ((int)playerStageList[number].transform.position.x == (int)checkPos.x
                && (int)playerStageList[number].transform.position.y == (int)checkPos.y)//ざっくりな位置  とりあえず
            {
                return number;
            }
        }
        return 0;
    }

    //どの台に乗っているか
    public int GetEnemyPosNumber(Vector3 checkPos)
    {
        var number = 0;
        for (number = 0; number < playerStageList.Count; number++)
        {
            if ((int)enemyStageList[number].transform.position.x == (int)checkPos.x
                && (int)enemyStageList[number].transform.position.y == (int)checkPos.y)//ざっくりな位置  とりあえず
            {
                return number;
            }
        }
        return 0;
    }
}
