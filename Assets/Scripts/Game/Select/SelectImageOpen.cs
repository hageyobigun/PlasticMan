using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SelectImageOpen
{
    private float firstScale = 0.7f;
    private float delayTime = 0.2f;

    public void ImageOpen(GameObject openImage)
    {
        var maxScale = openImage.transform.localScale;//元のサイズ取得

        openImage.transform.DOScale(maxScale * firstScale, 0f) //最初のサイズ
            .SetDelay(delayTime)
            .OnComplete(() =>
            {
                openImage.SetActive(true);//表示
                openImage.transform.DOScale(maxScale, 0.2f);//UI演出　大きくする。
            });

    }
}
