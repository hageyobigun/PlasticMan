using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
    [SerializeField] private Slider hpSlider = null;
    [SerializeField] private Slider mpSlider = null;

    //最大値設定
    public void SetMaxValue(int hpValue, int mpValue)
    {
        hpSlider.maxValue = hpValue;
        mpSlider.maxValue = mpValue;
    }

    //slider値変更
    public void UpdateHpSlider(int value)
    {
        hpSlider.value = value;
    }

    //slider値変更
    public void UpdateMpSlider(int value)
    {
        mpSlider.value = value;
    }
}
