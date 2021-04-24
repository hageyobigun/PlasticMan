using UnityEngine;
using Character;

public class FireBullet : BaseAttack
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float fireSpeed = 500f;
    [SerializeField] private float angle = 90f;//炎の絵の向き
    [SerializeField] private Category category = default;
    private int direction; //飛ばす向き

    // Start is called before the first frame update
    void Start()
    {
        SetDirection();

        transform.Rotate(new Vector3(0, 0, angle * direction));//炎の向き
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(fireSpeed * direction, 0));
        SoundManager.Instance.PlaySe("Fire");
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
