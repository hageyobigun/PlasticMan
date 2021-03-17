using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameObject fireBullet = null;
    [SerializeField] private GameObject bomb = null;
    [SerializeField] private GameObject barrier = null;

    [SerializeField]private int playerId = 0;

    //playerとenemy同じ playerId = 1 はplayer  -1 は enemy
    public void Awake()
    {
        if (barrier != null) barrier.SetActive(false);
    }

    //通常弾
    public void BulletAttack()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }

    //ファイヤ
    public void FireAttack()
    {
        //敵かプレイヤーかで向き調整
        Instantiate(fireBullet, this.transform.position + new Vector3(0, 1.0f, 0),
            Quaternion.Euler(0f, 0f, playerId * 90f));
    }

    //ボム
    public void BombAttack()
    {
        Instantiate(bomb, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }

    //バリア　インターバルは渡す
    public IEnumerator BarrierGuard(float interval)
    {
        barrier.SetActive(true);
        yield return new WaitForSeconds(interval);
        barrier.SetActive(false);
    }
}
