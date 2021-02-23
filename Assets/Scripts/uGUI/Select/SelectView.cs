using UnityEngine;
using UnityEngine.UI;

public class SelectView : MonoBehaviour
{
    //[SerializeField] private Image backImage = default;
    [SerializeField] private GameObject vsImage = default;
    [SerializeField] private GameObject rushImage = default;
    [SerializeField] private GameObject explainImage = default;
    [SerializeField] private GameObject soundImage = default;
    [SerializeField] private GameObject firsttmage = default;//最初の選択肢画面
    [SerializeField] private GameObject commonImage = default;

    [SerializeField] private EventSystemManeger _eventSystemManege  = default;

    private GameObject openImage;//開いているimage

    public void Awake()
    {
        openImage = null;
    }

    //Image表示
    public void OpneVsImage()
    {
        OpenImage(vsImage);
        _eventSystemManege.SetVsButton();
    }

    public void OpneRushImage()
    {
        OpenImage(rushImage);
        _eventSystemManege.SetRushButton();
    }

    public void OpneExplainImage()
    {
        OpenImage(explainImage);
        _eventSystemManege.SetExplainButton();
    }

    public void OpenSoundImage()
    {
        OpenImage(soundImage);
        _eventSystemManege.SetSoundButton();
    }

    private void OpenImage(GameObject selectImage)
    {
        openImage = selectImage;
        openImage.SetActive(true);
        commonImage.SetActive(true);
        firsttmage.SetActive(false);
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //Image閉じる
    public void CloseImage()
    {
        if (openImage != null) //開いてないときは押せないようにしておく
        {
            openImage.gameObject.SetActive(false);
            commonImage.SetActive(false);
            firsttmage.SetActive(true);
            _eventSystemManege.BackSelect();
            SoundManager.Instance.PlaySe("Cancel");
            openImage = null;
        }
    }
}
