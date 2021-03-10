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

    //ボタンをセット
    public void SetSelectButton(Button selectButton)
    {
        pushButton = eventSystem.currentSelectedGameObject;//押したボタン取得
        eventSystem.SetSelectedGameObject(selectButton.gameObject);//表示したimageのボタンを設定
    }

    public void SetSelectSlider(Slider selctSlider)
    {
        pushButton = eventSystem.currentSelectedGameObject;//押したslider取得
        eventSystem.SetSelectedGameObject(selctSlider.gameObject);//表示したimageのボタンを設定
    }

    //前のボタンセット
    public void BackSelectButton()
    {
        EventSystem.current.SetSelectedGameObject(pushButton);//戻す
    }
}
