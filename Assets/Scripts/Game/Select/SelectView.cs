using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectView : MonoBehaviour
{
    [SerializeField] private Image backImage = null;
    [SerializeField] private GameObject vsImage = null;
    [SerializeField] private GameObject rushImage = null;
    [SerializeField] private GameObject explainImage = null;

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

    private void OpenImage(GameObject selectImage)
    {
        openImage = selectImage;
        backImage.gameObject.SetActive(true);
        _selectImageOpen.ImageOpen(selectImage);
    }

    //Image閉じる
    public void CloseImage()
    {
        openImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(false);
    }
}
