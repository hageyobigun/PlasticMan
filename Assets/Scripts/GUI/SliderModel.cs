using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class SliderModel : MonoBehaviour
{
    // View(UI)
    public Slider hpSlider;
    public Text hpText;

    // View(UI)
    public Slider mpSlider;
    public Text mpText;

    // Model
    public ReactiveProperty<int> hp  = new ReactiveProperty<int>();
    public ReactiveProperty<int> mp = new ReactiveProperty<int>();

    void Start()
    {

        this.hp.Subscribe(countNum =>
        {
            if(hp.Value < 0)
            {
                hp.Value = 0;
            }
            this.hpText.text = countNum.ToString() + "/" + hpSlider.maxValue;
            this.hpSlider.value = countNum;
        });


        this.mp.Subscribe(countNum =>
        {
            if (mp.Value < 0)
            {
                mp.Value = 0;
            }
            this.mpText.text = countNum.ToString()  + "/" + mpSlider.maxValue;
            this.mpSlider.value = countNum;
        });
    }

    public void Initialize(int _hpValue, int _mpValue)
    {
        hpSlider.maxValue = _hpValue;
        mpSlider.maxValue = _mpValue;
        hp.Value = _hpValue;
        mp.Value = _mpValue;
    }
}
