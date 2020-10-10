using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{

    //いらないかも...
    public override void ApplyDamage()
    {
        //Destroy(gameObject);
    }

    public override void Attacked()
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }
}
