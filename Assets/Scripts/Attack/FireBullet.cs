using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(500, 0));
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var attackable = collision.GetComponent<Enemy.IAttackable>();
        var attacknotable = collision.GetComponent<IAttacknotable>();
        if (attacknotable != null)
        {
            Destroy(gameObject);
        }
        if (attackable != null)
        {
            attackable.Attacked(2);
        }
    }
}
