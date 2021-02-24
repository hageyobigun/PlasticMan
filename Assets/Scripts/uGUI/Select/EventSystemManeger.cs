using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

public class EventSystemManeger : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem = default;
    [SerializeField] private Image cursorImage = default;

    private SelectCursor _selectCursor;
    private ReactiveProperty<GameObject> selectObj = new ReactiveProperty<GameObject>();

    [SerializeField] private Button vsButton = default;
    [SerializeField] private Button rushButton = default;
    [SerializeField] private Button explainButton = default;
    [SerializeField] private Slider soundSlider = default;

    private GameObject pushButton;

    // Start is called before the first frame update
    void Start()
    {
        _selectCursor = new SelectCursor(cursorImage);

        this.UpdateAsObservable()
            .Subscribe(_ => selectObj.Value = eventSystem.currentSelectedGameObject.gameObject);//選択ボタン取得

        //選択しているものが変わったら
        selectObj
            .Skip(1)
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
