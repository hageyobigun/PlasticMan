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
    [SerializeField] private GameObject commonImage = default; //先駆し画面以外の共通のもの（戻るボタン）

    [SerializeField] private SelectButton _selectButton = default;//画面ごとにボタンをセットするためのもの

    private GameObject openImage;//開いているimage

    public void Awake()
    {
        openImage = null;
    }

    //Image表示
    public void OpneVsImage()
    {
        OpenImage(vsImage);
        _selectButton.SetVsButton();

    }

    public void OpneRushImage()
    {
        OpenImage(rushImage);
        _selectButton.SetRushButton();
    }

    public void OpneExplainImage()
    {
        OpenImage(explainImage);
        _selectButton.SetExplainButton();
    }

    public void OpenSoundImage()
    {
        OpenImage(soundImage);
        _selectButton.SetSoundButton();
    }

    //画面を開く
    private void OpenImage(GameObject selectImage)
    {
        openImage = selectImage; //開いている画面が何か入れる
        openImage.SetActive(true); //表示
        commonImage.SetActive(true);//共通のものを表示（戻るボタン）
        firsttmage.SetActive(false);//選択肢画面非表示
        SoundManager.Instance.PlaySe("NormalButton");
    }

    //Image閉じる
    public void CloseImage()
    {
        if (openImage != null) //開いてないときは押せないようにしておく
        {
            openImage.gameObject.SetActive(false); //開いていたもの非表示
            commonImage.SetActive(false); //共通のものを非表示（戻るボタン）
            firsttmage.SetActive(true); //選択肢画面
            _selectButton.SetBackSelectButton();//　前の画面のボタンセット
            SoundManager.Instance.PlaySe("Cancel");
            openImage = null;
        }
    }
}
