using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerAttack:MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;

    public void BulletAttack()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0,0.6f,0), Quaternion.identity);
    }
}
