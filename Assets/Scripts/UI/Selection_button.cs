using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;


public class Selection_button : MonoBehaviour
{
    [SerializeField] private Image cursor = null;
    [SerializeField] EventSystem eventSystem = null;
    private GameObject selectedObj;

    private void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
                {
                    selectedObj = eventSystem.currentSelectedGameObject.gameObject;
                    cursor.transform.position = selectedObj.transform.position + new Vector3(-330, 0, 0);//やり方変えるかも
                });
    }



    public void VsButton()
    {
        Debug.Log("1vs1");
    }

    public void BossRushButton()
    {
        Debug.Log("boss");
    }

    public void Explanation()
    {
        Debug.Log("ex");
    }

}
