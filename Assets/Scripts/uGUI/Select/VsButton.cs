using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game;

public class VsButton : MonoBehaviour
{
    [SerializeField] private List<Button> enemyButtons = new List<Button>();

    void Awake()
    {
        //全部のボタンセット
        for(int buttonNUmber = 0; buttonNUmber < enemyButtons.Count; buttonNUmber++)
        {
            SetButton(buttonNUmber);
        }
    }

    //ボタン毎に敵番号を入れる
    private void SetButton(int number)
    {
        enemyButtons[number].OnClickAsObservable()
            .Subscribe(_ =>
            {
                SelectEnemy(number);
                SoundManager.Instance.PlaySe("NormalButton");
            });
    }

    //敵番号を入れる
    private void SelectEnemy(int enemyNUmber)
    {
        GameManeger.Instance.GetEnemyNumber = enemyNUmber;
    }

}
