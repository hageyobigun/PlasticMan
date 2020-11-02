using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 target;
    private float deg;
    private Animator animator;
    private BoxCollider2D boxCollider2d;


    //雑なので改善予定？
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        boxCollider2d.enabled = false;
        SetTarget(this.transform.position + new Vector3(9.4f, 0, 0), 60);

    }

    IEnumerator ThrowBall()
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float x = 0; x <= this.target.x; x += 0.3f)
        {
            float y = a * x * x + b * x;
            transform.position = new Vector3(x, y, 0) + offset;
            yield return null;
        }
        boxCollider2d.enabled = true;
        animator.SetTrigger("explosionTrigger");
        gameObject.transform.localScale = new Vector3(2, 2, 1);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }

    public void SetTarget(Vector3 target, float deg)
    {
        this.offset = transform.position;
        this.target = target - this.offset;
        this.deg = deg;

        StartCoroutine("ThrowBall");
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
            attackable.Attacked(4);
        }
    }
}
