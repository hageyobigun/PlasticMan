using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class EventSystemManeger : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem = default;
    [SerializeField] private Image cursorImage = default;

    private SelectCursor _selectCursor;

    [SerializeField] private Button vsButton = default;
    [SerializeField] private Button rushButton = default;
    [SerializeField] private Button explainButton = default;
    [SerializeField] private Slider soundSlider = default;

    private GameObject pushButton;

    // Start is called before the first frame update
    void Start()
    {
        _selectCursor = new SelectCursor(cursorImage);

        //選択しているものが変わったらカーソル移動
        this.ObserveEveryValueChanged(select => eventSystem.currentSelectedGameObject)
            .Where(select => select != null)
            .Subscribe(select => _selectCursor.CursorMove(select));
    }

    public void SetVsButton()=>  SetSelectButton(vsButton);

    public void SetRushButton() => SetSelectButton(rushButton);

    public void SetExplainButton() => SetSelectButton(explainButton);

    //サウンドはsliderなので別のもの
    public void SetSoundButton()
    {
        pushButton = eventSystem.currentSelectedGameObject;//押したslider取得
        eventSystem.SetSelectedGameObject(soundSlider.gameObject);//表示したimageのボタンを設定
    }
    //ボタンをセット
    private void SetSelectButton(Button selectButton)
    {
        pushButton = eventSystem.currentSelectedGameObject;//押したボタン取得
        eventSystem.SetSelectedGameObject(selectButton.gameObject);//表示したimageのボタンを設定
    }

    public void BackSelect()
    {
        EventSystem.current.SetSelectedGameObject(pushButton);//戻す
    }
}
