using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;


public class SelectButton : MonoBehaviour
{
    private int selectNumber; //どのボタンを押したか
    [SerializeField] private SelectView _selectView = null;

    public void VsButton() //1vs1
    {
        selectNumber = 0;
        _selectView.OnDiplay(selectNumber);
    }

    public void BossRushButton() //ボスラッシュ
    {
        selectNumber = 1;
        _selectView.OnDiplay(selectNumber);
    }

    public void Explanation() //操作説明など
    {
        selectNumber = 2;
        _selectView.OnDiplay(selectNumber);
    }

    public void Close()
    {
        _selectView.CloseImage(selectNumber);
    }

}
