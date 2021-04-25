namespace Character
{
    //プレイヤーか敵かどうか
    public enum Category
    {
        Player,
        Enemy
    }

    //攻撃や防御の状態
    public enum State
    {
        Normal,
        Bullet_Attack,
        Fire_Attack,
        Bomb_Attack,
        Barrier
    }
}
