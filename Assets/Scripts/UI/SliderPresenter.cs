using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class SliderPresenter : MonoBehaviour
{
    // View(UI)
    public Slider hpSlider;
    public Text hpText;

    // View(UI)
    public Slider mpSlider;
    public Text mpText;

    // Model
    public ReactiveProperty<int> hp { get; private set; } // 外部からは取得のみできる

    public ReactiveProperty<int> mp { get; private set; } // 外部からは取得のみできる

    public SliderPresenter(int _hpValue, int _mpValue)
    {
        hp.Value = _hpValue;
        hpSlider.maxValue = _hpValue;
        mp.Value = _mpValue;
        mpSlider.maxValue = _mpValue;

    }

    void Start()
    {

        this.hp.Subscribe(countNum =>
        {
            this.hpText.text = countNum.ToString();
            this.hpSlider.value = countNum;   
        });


        this.mp.Subscribe(countNum =>
        {
            this.mpText.text = countNum.ToString();
            this.mpSlider.value = countNum;
        });
    }
}
