using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

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

    //categoryによってどっちのステージを返すか決める

    //stageListを返す
    public List<GameObject> GetStageList(Category category)
    {
        if (category == Category.Player)//plpayer
        {
            return playerStageList;
        }
        else if (category == Category.Enemy)//enemy
        {
            return enemyStageList;
        }
        return null;
    }

    //どの台に乗っているかを返す
    public int GetStagePosNumber(Vector3 checkPos, Category category)
    {
        var number = 0;
        var stageList = GetStageList(category);
        for (number = 0; number < stageList.Count; number++)
        {
            if ((int)stageList[number].transform.position.x == (int)checkPos.x
                && (int)stageList[number].transform.position.y == (int)checkPos.y)//ざっくりな位置  とりあえず
            {
                return number;
            }
        }
        return 0;
    }

    //指定されたブロックの返す
    public GameObject GetStageBlock(int number, Category category)
    {
        var stageList = GetStageList(category);
        return stageList[number];
    }

}
