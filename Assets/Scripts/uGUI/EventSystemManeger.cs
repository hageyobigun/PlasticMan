﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class EventSystemManeger : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem = default;
    [SerializeField] private Image cursorImage = default; //カーソル

    private List<GameObject> pushButtonList = new List<GameObject>(); //押したボタンを入れていくリスト
    private int pushButtonListNumber;  //入れていったボタンの番号

    private SelectCursor _selectCursor;

    void Awake()
    {
        _selectCursor = new SelectCursor(cursorImage);
        pushButtonListNumber = -1;//リストに合わせて最初は０

        //選択しているものが変わったらカーソル移動
        this.ObserveEveryValueChanged(select => eventSystem.currentSelectedGameObject)
            .Where(_ => cursorImage != null)
            .Where(select => select != null)
            .Subscribe(select => _selectCursor.CursorMove(select));
    }

    //押したボタン（オブジェクト）をセット
    public void SetSelectObj(GameObject selectObj)
    {
        //押したボタンを順に入れて行く
        pushButtonList.Add(eventSystem.currentSelectedGameObject);
        pushButtonListNumber++;
        eventSystem.SetSelectedGameObject(selectObj);//表示したimageのボタンを設定
    }

    //前のボタンセット
    public void BackSelectButton()
    {
        if (pushButtonList.Count >= 1)//空っぽじゃない場合
        {
            //戻るたびにリストから消して行く
            EventSystem.current.SetSelectedGameObject(pushButtonList[pushButtonListNumber]);//戻す
            pushButtonList.RemoveAt(pushButtonListNumber);
            pushButtonListNumber--;
        }
    }

    //押したボタンではなく指定したボタンをセットする
    public void FirstSetObj(GameObject setObj)
    {
        eventSystem.SetSelectedGameObject(setObj);//表示したimageのボタンを設定
    }

    //リストも登録も解除で初期状態に
    public void Clear()
    {
        eventSystem.SetSelectedGameObject(null);//表示したimageのボタンを設定
        pushButtonList = new List<GameObject>();
        pushButtonListNumber = -1;//リストに合わせて最初は０
    }
}
