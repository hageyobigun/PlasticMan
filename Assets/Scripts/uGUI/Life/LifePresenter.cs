using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LifePresenter : MonoBehaviour
{
    // View
    [SerializeField] private LifeView lifeView = default;
    // Model
    private LifeModel lifeModel;

    //初期化 hpとmpはプレイヤーの方で設定
    public void Initialize(int hpValue, int mpValue)
    {
        lifeModel = new LifeModel(hpValue, mpValue);
        lifeView.SetMaxValue(hpValue, mpValue);

        lifeModel.hp
            .Subscribe(value => lifeView.UpdateHpSlider(value));

        lifeModel.mp
            .Subscribe(value => lifeView.UpdateMpSlider(value));
    }

    //hp変更
    public void OnChangeHpLife(int value)
    {
        lifeModel.UpdateHpLife(value);
    }

    //mp変更
    public void OnChangeMpLife(int value)
    {
        lifeModel.UpdateMpLife(value);
    }
}
