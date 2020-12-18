using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    public int damagePower;　//与えるダメージ量
    public int playerId;

    private  void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerId == 1)//player
        {
            var attackable = collision.GetComponent<Enemy.IAttackable>();
            if (attackable != null)
            {
                attackable.Attacked(damagePower);
                Destroy(gameObject);
            }
        }
        else if (playerId == -1)//enemy
        {
            var attackable = collision.GetComponent<Player.IAttackable>();
            if (attackable != null)
            {
                attackable.Attacked(damagePower);
                Destroy(gameObject);
            }
        }

        var attacknotable = collision.GetComponent<IAttacknotable>();

        if (attacknotable != null)
        {
            if (attacknotable.barriered(playerId))
            {
                Destroy(gameObject);
            }
        }
    }
}
