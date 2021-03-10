using UnityEngine;
using DG.Tweening;


public class MenuButtonImageOpen
{
    private float firstScale = 0.7f;
    private float openSpeed = 0.2f;

    public void ImageOpen(GameObject openImage)
    {
        var maxScale = openImage.transform.localScale;//元のサイズ取得

        var openSequence = DOTween.Sequence();

        openSequence.Append(
            openImage.transform.DOScale(maxScale * firstScale, 0f) //最初のサイズを小さくする
            .OnComplete(() => openImage.SetActive(true)));//表示

        openSequence.Append(openImage.transform.DOScale(maxScale, openSpeed)); //拡大
    }

}
