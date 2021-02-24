using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SoundBar : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider = default;
    [SerializeField] private Slider seSlider = default;

    // Start is called before the first frame update
    void Start()
    {
        bgmSlider.value = SoundManager.Instance.volume.bgm;
        seSlider.value = SoundManager.Instance.volume.se;

        bgmSlider.OnValueChangedAsObservable()
            .Subscribe(_ => SoundManager.Instance.ChangeBgmVolume(bgmSlider.value));

        seSlider.OnValueChangedAsObservable()
            .Subscribe(_ => SoundManager.Instance.ChangeSeVolume(seSlider.value));
    }


}
