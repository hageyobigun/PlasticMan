using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{

    public override void Attacked()
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }
}
