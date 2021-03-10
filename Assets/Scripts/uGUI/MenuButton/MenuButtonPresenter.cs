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
   
    // Start is called before the first frame update
    void Awake()
    {
        _menuButtonModel = new MenuButtonModel();


        //シーン移動開始
        yesButton.OnClickAsObservable()
            .Subscribe(_ => _menuButtonView.YesButton(_menuButtonModel.moveScene));

        //キャンセル、閉じる
        noButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _menuButtonView.NoButton();
                _eventSystemManeger.BackSelectButton();
            });

        //閉じるボタン
        this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Cancel"))
            .Subscribe(_ =>
            {
                _menuButtonView.NoButton();
                _eventSystemManeger.BackSelectButton();
            });
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
        _eventSystemManeger.SetSelectButton(yesButton);
    }

}
