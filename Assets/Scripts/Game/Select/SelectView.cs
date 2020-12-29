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
    [SerializeField] private GameObject ruhsImage = null;
    [SerializeField] private GameObject explainImage = null;

    //private SelectImageOpen _selectImageOpen;
    private GameObject openImage;

    public void Awake()
    {
        //_selectImageOpen = new SelectImageOpen();
        openImage = null;
    }

    //Image表示

    public void OpneVsImage()
    {
        vsImage.SetActive(true);
        openImage = vsImage;
        backImage.gameObject.SetActive(true);
    }

    public void OpneRushImage()
    {
        ruhsImage.SetActive(true);
        openImage = ruhsImage;
        backImage.gameObject.SetActive(true);
    }

    public void OpneExplainImage()
    {
        explainImage.SetActive(true);
        openImage = explainImage;
        backImage.gameObject.SetActive(true);
    }


    //Image閉じる
    public void CloseImage()
    {
        openImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }
}
