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
    [SerializeField] private Image backImage = null;

    private SelectImageOpen _selectImageOpen;

    public void Awake()
    {
        _selectImageOpen = new SelectImageOpen();
        InvisibleImage();

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                var selectedObj = EventSystem.current.currentSelectedGameObject.gameObject;
                cursor.transform.position = selectedObj.transform.position + new Vector3(-330, 0, 0);//やり方変えるかも
                cursor.transform.Rotate(new Vector3(2, 0, 0));//回転演出（消すかも）
            });
    }

    //Image表示
    public void OnDiplay(int selectNumber)
    {
        selectImage[selectNumber].SetActive(true);
        backImage.gameObject.SetActive(true);
        StartCoroutine(_selectImageOpen.OpenImage(selectImage[selectNumber]));
        EventSystem.current.SetSelectedGameObject(selectButton[selectNumber]);
    }

    //Image閉じる
    public void CloseImage(int selectNumber)
    {
        backImage.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelectButton);
        selectImage[selectNumber].SetActive(false);
    }

    //最初に全部非表示
    public void InvisibleImage()
    {
        for (int i = 0; i < selectImage.Count; i++)
        {
            selectImage[i].SetActive(false);
        }
        backImage.gameObject.SetActive(false);
    }
}
