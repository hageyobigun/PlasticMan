using System.Collections;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameObject fireBullet = null;
    [SerializeField] private GameObject bomb = null;
    [SerializeField] private GameObject barrier = null;

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
        Instantiate(fireBullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
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
