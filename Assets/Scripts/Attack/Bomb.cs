using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using Game;

public class Bomb : MonoBehaviour
{

    [SerializeField] private int playerId = 0;    //1:player -1:enemy
    [SerializeField] private float flightTime = 1.0f;  //滞空時間
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private GameObject explosion = null;

    private Sequence sequence;

    private List<GameObject> stageList = new List<GameObject>();

    private int targetStageNumber;

    public void Start()
    {
        ThrowBomb();
        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Pause)//止める
            .Subscribe(_ => sequence.Pause());

        this.UpdateAsObservable()
            .Where(_ => GameManeger.Instance.currentGameStates.Value == GameState.Play)//動かす
            .Subscribe(_ => sequence.Play());

    }

    //爆弾発車
    public void ThrowBomb()
    {
        var startPos = transform.position; // 初期位置
        //どうしよう...
        if (playerId == 1)//plpayer
        {
            targetStageNumber = StageManager.Instance.GetPlayerPosNumber(this.transform.position);
            stageList = StageManager.Instance.GetEnemyStageList;
        }
        else if(playerId == -1)//enemy
        {
            targetStageNumber = StageManager.Instance.GetEnemyPosNumber(this.transform.position);
            stageList = StageManager.Instance.GetPlayerStageList;
        }

        var endPos = stageList[targetStageNumber].transform.position;//爆弾投下位置
        sequence = DOTween.Sequence();

        sequence.Append(this.transform.DOJump(endPos, jumpPower, 1, flightTime)
            .OnComplete(() =>
            {
                Instance_explosion();
                Destroy(gameObject);
            })
        );

    }

    //とりあえず後で改善予定
    private void Instance_explosion()
    {
        SoundManager.Instance.PlaySe("Bomb");
        var setPos = targetStageNumber % 3;
        for(int i = 0; i < 3; i++)
        {
            //三つ縦に爆発
            Instantiate(explosion, stageList[setPos + i * 3].transform.position + new Vector3(0, 0, -1)
                , Quaternion.identity);
        }
    }

}
