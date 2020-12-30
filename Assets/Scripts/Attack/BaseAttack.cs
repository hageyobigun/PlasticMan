using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃の基底クラス
public abstract class BaseAttack : MonoBehaviour
{
    public int damagePower;　//与えるダメージ量
    public int playerId; //プレイヤーID player:1 enemy:-1
    private Flashing _flashing; //ダメージを受けた際の点滅処理

    private void Awake()
    {
        _flashing = new Flashing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerId == 1)//player
        {
            var attackable = collision.GetComponent<Enemy.IAttackable>();
            if (attackable != null)
            {
                _flashing.Flash(collision.GetComponent<SpriteRenderer>());
                attackable.Attacked(damagePower);
                Destroy(gameObject);
            }
        }
        else if (playerId == -1)//enemy
        {
            var attackable = collision.GetComponent<Player.IAttackable>();
            if (attackable != null)
            {
                _flashing.Flash(collision.GetComponent<SpriteRenderer>());
                attackable.Attacked(damagePower);
                Destroy(gameObject);
            }
        }

        var attacknotable = collision.GetComponent<IAttacknotable>(); //バリア

        if (attacknotable != null)
        {
            if (attacknotable.barriered(playerId))
            {
                Destroy(gameObject);
            }
        }
    }
}
