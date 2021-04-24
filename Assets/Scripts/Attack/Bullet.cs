using UnityEngine;
using Character;

public class Bullet : BaseAttack
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float bulletSpeed = 1000f;
    [SerializeField] private Category category = default;
    private int direction; //飛ばす向き

    // Start is called before the first frame update
    void Start()
    {
        SetDirection();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(bulletSpeed * direction, 0));
        SoundManager.Instance.PlaySe("Shot");
    }

    //向きをplayer側かenemy側かで調整
    private void SetDirection()
    {
        if (category == Category.Player)
        {
            direction = 1;
        }
        else if (category == Category.Enemy)
        {
            direction = -1;
        }
    }

    //画面外削除
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
