using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{

    [SerializeField] private int playerId = 0;    //1:player -1:enemy
    [SerializeField] private float flightTime = 1.0f;  //滞空時間
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private GameObject explosion = null;

    public void Start()
    {
        ThrowBomb();
    }

    //爆弾発車
    public void ThrowBomb()
    {
        var startPos = transform.position; // 初期位置
        var endPos = startPos + new Vector3(9.4f * playerId, 0, 0);//爆弾投下位置

        this.transform.DOJump(endPos, jumpPower, 1, flightTime)
            .OnComplete(() =>
            {
                Instance_explosion();
                Destroy(gameObject);
            });
    }

    //とりあえず後で改善予定
    private void Instance_explosion()
    {
        SoundManager.Instance.PlaySe("Bomb");
        Instantiate(explosion, new Vector3(transform.position.x, 3.0f, transform.position.z), Quaternion.identity);
        Instantiate(explosion, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        Instantiate(explosion, new Vector3(transform.position.x, -2.0f, transform.position.z), Quaternion.identity);
    }

}
