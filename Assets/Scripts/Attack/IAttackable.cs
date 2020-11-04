namespace Enemy
{
    public interface IAttackable
    {
        void Attacked(float damage);
    }
}


namespace Player
{
    public interface IAttackable
    {
        void Attacked(float damage);
    }
}
