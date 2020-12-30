using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BaseAttack
{
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(500 * playerId, 0));
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
