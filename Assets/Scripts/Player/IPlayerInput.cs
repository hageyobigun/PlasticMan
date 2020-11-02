using UnityEngine;

public interface IPlayerInput
{
    int Inputting();

    bool IsBulltetAttack();

    bool IsFireAttack();

    bool IsBombAttack();

    bool IsBarrier();
}
