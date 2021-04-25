using UnityEngine;
using DG.Tweening;

public class ResultImageOpen
{
    private float openSpeed = 0.8f; //拡大速度

    //resultMenuの開く演出
    public void ResultOpen(GameObject openImage)
    {
        var maxScale = openImage.transform.localScale.x;//元のサイズ取得

        var openSequence = DOTween.Sequence();

        openSequence.Append(
            openImage.transform.DOScaleX(0, 0f) //最初のサイズを0にする
            .OnComplete(() => openImage.SetActive(true)));//表示
        openSequence.Append(openImage.transform.DOScaleX(maxScale, openSpeed)); //拡大
    }
}
