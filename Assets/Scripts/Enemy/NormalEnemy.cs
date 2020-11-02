using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{

    public override void Attacked(float damage)
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }
}
