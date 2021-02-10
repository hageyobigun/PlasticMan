using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeView : MonoBehaviour
{
    [SerializeField] private Slider hpSlider = default;
    [SerializeField] private Slider mpSlider = default;

    //[SerializeField] private Slider mpBackSlider = default;


    //最大値設定
    public void SetMaxValue(int hpValue, int mpValue)
    {
        hpSlider.maxValue = hpValue;
        mpSlider.maxValue = mpValue;
        //mpBackSlider.maxValue = mpValue;
    }

    //slider値変更
    public void UpdateHpSlider(int value)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(hpSlider.DOValue(value, 1.0f).SetEase(Ease.Linear));
    }

    //slider値変更
    public void UpdateMpSlider(int value)
    {
        //mpSlider.value = value;
        var sequence = DOTween.Sequence();
        sequence.Append(mpSlider.DOValue(value, 1.0f).SetEase(Ease.Linear));
        //sequence.Append(mpBackSlider.DOValue(value, 1.0f).SetEase(Ease.Linear).SetDelay(1.0f));
    }


}
