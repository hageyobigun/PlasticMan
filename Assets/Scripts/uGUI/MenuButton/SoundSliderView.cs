using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSliderView : MonoBehaviour
{
    [SerializeField] private GameObject soundSliderImage = default; //音量調整
    private MenuButtonImageOpen _menuButtonImageOpen;//開く演出

    // Start is called before the first frame update
    void Awake()
    {
        _menuButtonImageOpen = new MenuButtonImageOpen();
    }

    //音量調整出す
    public void SoundButton()
    {
        soundSliderImage.SetActive(true);
        _menuButtonImageOpen.ImageOpen(soundSliderImage);//開く演出
        SoundManager.Instance.PlaySe("NormalButton");
    }


    //閉じるボタン(close)
    public void CloseButton()
    {
        if (soundSliderImage.activeInHierarchy == true) //何も開いてないときは動かない
        {
            soundSliderImage.SetActive(false);//表示していたものを閉じる
            SoundManager.Instance.PlaySe("Cancel");
        }
    }
}
