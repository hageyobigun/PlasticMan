using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    [SerializeField]private GameObject playerStageBlockParent = null;
    [SerializeField]private GameObject enemyStageBlockParent = null;
    public List<GameObject> playerStageList = new List<GameObject>();
    public List<GameObject> enemyStageList = new List<GameObject>();

    public StageCheck _stageCheck;


    // Start is called before the first frame update
    private void Start()
    {
        getStage();
        _stageCheck = new StageCheck();
    }

    private void getStage()
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


}
