using UnityEngine;

//入力インターフェース
public interface IPlayerInput
{
    int Inputting();

    bool IsBulltetAttack();

    bool IsFireAttack();

    bool IsBombAttack();

    bool IsBarrier();
}
