using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private float flightTime = 1.0f;  //滞空時間
    [SerializeField] float speedRate = 1;   //滞空時間を基準とした移動速度倍率
    private const float gravity = -9.8f;    //重力

    private List<GameObject> stageList = new List<GameObject>();
    private int targetStageNumber;
    [SerializeField] private Category category = default;

    public void Start()
    {
        StartCoroutine(ThrowBomb(flightTime, speedRate, gravity));
    }

    //ボム発射
    private IEnumerator ThrowBomb(float flightTime, float speedRate, float gravity)
    {

        var startPos = transform.position; // 初期位置
        //どうしよう...
        targetStageNumber = StageManager.Instance.GetStagePosNumber(this.transform.position, category);
        if (category == Category.Player)//plpayer
        {
            stageList = StageManager.Instance.GetStageList(Category.Enemy);
        }
        else if (category == Category.Enemy)//enemy
        {
            stageList = StageManager.Instance.GetStageList(Category.Player);
        }

        var endPos = stageList[targetStageNumber].transform.position;//爆弾投下位置
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            transform.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        transform.position = endPos;
        Instance_explosion();
        Destroy(gameObject);
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
