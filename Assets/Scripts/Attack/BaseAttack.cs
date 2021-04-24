using UnityEngine;

//攻撃の基底クラス
public abstract class BaseAttack : MonoBehaviour
{
    public int damagePower;　//与えるダメージ量
    private Flashing _flashing; //ダメージを受けた際の点滅処理

    private void Awake()
    {
        _flashing = new Flashing();
    }

    //衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {

        var attacknotable = collision.GetComponent<IAttacknotable>(); //バリア
        if (attacknotable != null)
        {
            //バリアに当たった
            attacknotable.barriered();
            Destroy(gameObject);
        }
        
        //攻撃hit (enemyとplayerの判別はレイヤーで当たり判定消している）
        var attackable = collision.GetComponent<IAttackable>();
        if (attackable != null)
        {
            _flashing.Flash(collision.GetComponent<SpriteRenderer>());
            attackable.Attacked(damagePower);
            SoundManager.Instance.PlaySe("Hit");
            Destroy(gameObject);
        }
    }
}
