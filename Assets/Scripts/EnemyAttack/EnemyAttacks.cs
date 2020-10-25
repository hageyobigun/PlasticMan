using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;

    public void BulletAttack()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
    }
}
