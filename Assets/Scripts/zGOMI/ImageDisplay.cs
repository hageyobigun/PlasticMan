using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisplay
{
    private GameObject openImage;//開いているimage

    private ImageDisplay()
    {

    }

    //画面を開く
    private void OpenImage(GameObject selectImage)
    {
        openImage = selectImage; //開いている画面が何か入れる
        openImage.SetActive(true); //表示
        //commonImage.SetActive(true);//共通のものを表示（戻るボタン）
        //firsttmage.SetActive(false);//選択肢画面非表示
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //Image閉じる
    public void CloseImage()
    {
        if (openImage != null) //開いてないときは押せないようにしておく
        {
            openImage.gameObject.SetActive(false); //開いていたもの非表示
            //commonImage.SetActive(false); //共通のものを非表示（戻るボタン）
            //firsttmage.SetActive(true); //選択肢画面
            //_eventSystemManege.BackSelect();
            SoundManager.Instance.PlaySe("Cancel");
            openImage = null;
        }
    }
}
