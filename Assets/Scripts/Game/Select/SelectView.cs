using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SelectView : MonoBehaviour
{
    [SerializeField] private Image backImage = default;
    [SerializeField] private GameObject vsImage = default;
    [SerializeField] private GameObject rushImage = default;
    [SerializeField] private GameObject explainImage = default;
    [SerializeField] private GameObject soundImage = default;

    private SelectImageOpen _selectImageOpen;
    private GameObject openImage;

    public void Awake()
    {
        _selectImageOpen = new SelectImageOpen();
        openImage = null;
    }

    //Image表示
    public void OpneVsImage()
    {
        OpenImage(vsImage);
    }

    public void OpneRushImage()
    {
        OpenImage(rushImage);
    }

    public void OpneExplainImage()
    {
        OpenImage(explainImage);
    }

    public void OpenSoundImage()
    {
        OpenImage(soundImage);
    }

    private void OpenImage(GameObject selectImage)
    {
        openImage = selectImage;
        backImage.gameObject.SetActive(true);
        _selectImageOpen.ImageOpen(selectImage);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //Image閉じる
    public void CloseImage()
    {
        openImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(false);
        SoundManager.Instance.PlaySe("Cancel");
    }
}
