using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    [SerializeField] private int playerId = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(1000 * playerId, 0));
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerId == 1)//player
        {
            var attackable = collision.GetComponent<Enemy.IAttackable>();
            if (attackable != null)
            {
                attackable.Attacked(1);
                Destroy(gameObject);
            }
        }
        else if(playerId == -1)//enemy
        {
            var attackable = collision.GetComponent<Player.IAttackable>();
            if (attackable != null)
            {
                attackable.Attacked(1);
                Destroy(gameObject);
            }
        }

        var attacknotable = collision.GetComponent<IAttacknotable>();

        if (attacknotable != null)
        {
            Destroy(gameObject);
        }
    }
}
