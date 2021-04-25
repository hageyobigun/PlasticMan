using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeView : MonoBehaviour
{
    [SerializeField] private Slider hpSlider = default;
    [SerializeField] private Slider mpSlider = default;

    private float sliderMoveSpeed = 1.0f;//スライダーの値が動く速度

    //最大値設定
    public void SetMaxValue(int hpValue, int mpValue)
    {
        hpSlider.maxValue = hpValue;
        mpSlider.maxValue = mpValue;
    }

    //slider値変更
    public void UpdateHpSlider(int value)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(hpSlider.DOValue(value, sliderMoveSpeed).SetEase(Ease.Linear));
    }

    //slider値変更
    public void UpdateMpSlider(int value)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(mpSlider.DOValue(value, sliderMoveSpeed).SetEase(Ease.Linear));
    }


}
