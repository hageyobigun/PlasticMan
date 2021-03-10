using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EventSystemPushButton
{
    private List<GameObject> pushButtonList = new List<GameObject>();
    private int pushButtonListNumber;
    private EventSystem eventSystem = default;

    public EventSystemPushButton(EventSystem _eventSystem)
    {
        eventSystem = _eventSystem;
    }

    //押したボタン取得
    public void SetPushObj()
    {
        //押したボタンを順に入れて行く
        pushButtonList.Add(eventSystem.currentSelectedGameObject);
        pushButtonListNumber++;
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
}
