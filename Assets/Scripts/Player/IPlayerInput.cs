using UnityEngine;

public interface IPlayerInput
{
    void    Inputting();

    Vector3 MoveDirection();

    bool    IsAttack();
}
