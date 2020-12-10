using UnityEngine;
using UniRx;


public class LifeModel
{
    // Model
    public ReactiveProperty<int> hp;

    public ReactiveProperty<int> mp;

    //初期化
    public LifeModel(int hpValue, int mpValue)
    {
        hp = new ReactiveProperty<int>(hpValue);
        mp = new ReactiveProperty<int>(mpValue);
    }


    public void UpdateHpLife(int value)
    {
        hp.Value = value;
    }

    public void UpdateMpLife(int value)
    {
        mp.Value = value;
    }
}
