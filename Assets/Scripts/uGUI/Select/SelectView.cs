using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectView : MonoBehaviour
{
    //[SerializeField] private Image backImage = default;
    [SerializeField] private GameObject vsImage = default;
    [SerializeField] private GameObject rushImage = default;
    [SerializeField] private GameObject explainImage = default;
    [SerializeField] private GameObject soundImage = default;

    [SerializeField] private GameObject firsttmage = default;

    private GameObject openImage;

    public void Awake()
    {
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
        selectImage.SetActive(true);
        firsttmage.SetActive(false);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //Image閉じる
    public void CloseImage()
    {
        if (openImage != null)
        {
            openImage.gameObject.SetActive(false);
            firsttmage.SetActive(true);
            openImage = null;
            SoundManager.Instance.PlaySe("Cancel");
        }
    }
}
