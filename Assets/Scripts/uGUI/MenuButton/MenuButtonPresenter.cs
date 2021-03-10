using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Game;
using System;


public class MenuButtonPresenter : MonoBehaviour
{
    [SerializeField] private Button yesButton = default;
    [SerializeField] private Button noButton = default;

    [SerializeField] private MenuButtonView _menuButtonView = default;
    private MenuButtonModel _menuButtonModel;

    [SerializeField] private EventSystemManeger _eventSystemManeger = default;

    private bool isOpen;//確認画面を開いているかどうか
   
    // Start is called before the first frame update
    void Awake()
    {
        _menuButtonModel = new MenuButtonModel();

        isOpen = false;

        //シーン移動開始(yesボタン）
        yesButton.OnClickAsObservable()
            .Where(_ => isOpen)
            .Subscribe(_ => _menuButtonView.YesButton(_menuButtonModel.moveScene));

        //キャンセル、閉じる
        noButton.OnClickAsObservable()
            .Where(_ => isOpen)
            .Subscribe(_ => CloseButton());

        //閉じるボタン
        this.UpdateAsObservable()
            .Where(_ => isOpen)
            .Where(_ => Input.GetButtonDown("Cancel"))
            .Subscribe(_ => CloseButton());
    }

    //閉じるボタン
    private void CloseButton()
    {
        _menuButtonView.NoButton();
        _eventSystemManeger.BackSelectButton();
        isOpen = false;
    }

    //ボタンクリック
    public void ButtonOnClick(string gameStateName)
    {
        //文字をenumに変換
        var gameState = (GameState)Enum.Parse(typeof(GameState), gameStateName);
        _menuButtonModel.ChangeMoveScene(gameState);
        //表示テキスト変更
        _menuButtonView.MenuButton(_menuButtonModel.menuText);
        //eventsystemセット
        _eventSystemManeger.SetSelectObj(yesButton.gameObject);

        isOpen = true;
    }

}
