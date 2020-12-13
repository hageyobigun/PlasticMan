using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectView : MonoBehaviour
{
    [SerializeField] private Image cursor = null;
    [SerializeField] private GameObject firstSelectButton = null;
    [SerializeField] private List<GameObject> selectImage = new List<GameObject>(); //ボタンを押した時表示される
    [SerializeField] private List<GameObject> selectButton = new List<GameObject>(); //使うボタン変更用

    public void Awake()
    {
        for (int i = 0; i < selectImage.Count; i++) //最初に全部非表示
        {
            selectImage[i].SetActive(false);
        }

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                var selectedObj = EventSystem.current.currentSelectedGameObject.gameObject;
                cursor.transform.position = selectedObj.transform.position + new Vector3(-330, 0, 0);//やり方変えるかも
            });
    }

    public void OnDiplay(int selectNumber)
    {
        selectImage[selectNumber].SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectButton[selectNumber]);
    }

    public void CloseImage(int selectNumber)
    {
        EventSystem.current.SetSelectedGameObject(firstSelectButton);
        selectImage[selectNumber].SetActive(false);
    }
}
