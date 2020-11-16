﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private int playerId = 0;

    private float gravity = -9.8f;    //重力
    [SerializeField] private float flightTime = 10;  //滞空時間
    [SerializeField] private float speedRate = 100;   //滞空時間を基準とした移動速度倍率
    [SerializeField] private GameObject explosion = null;

    //雑なので改善予定？
    public void Start()
    {
        StartCoroutine(ThrowBomb());
    }

    public IEnumerator ThrowBomb()
    {
        var startPos = transform.position; // 初期位置
        var endPos = startPos + new Vector3(9.4f * playerId, 0, 0);
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
        Instantiate(explosion, new Vector3(transform.position.x, 3.0f, transform.position.z), Quaternion.identity);
        Instantiate(explosion, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        Instantiate(explosion, new Vector3(transform.position.x, -2.0f, transform.position.z), Quaternion.identity);
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (playerId == 1)//player
    //    {
    //        var attackable = collision.GetComponent<Enemy.IAttackable>();
    //        if (attackable != null)
    //        {
    //            attackable.Attacked(4);
    //        }
    //    }
    //    else if (playerId == -1)//enemy
    //    {
    //        var attackable = collision.GetComponent<Player.IAttackable>();
    //        if (attackable != null)
    //        {
    //            attackable.Attacked(4);
    //        }
    //    }

    //    var attacknotable = collision.GetComponent<IAttacknotable>();
    //    if (attacknotable != null)
    //    {
    //        if (attacknotable.barriered(playerId))
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
