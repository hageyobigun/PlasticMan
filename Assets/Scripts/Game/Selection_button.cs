using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;


public class Selection_button : MonoBehaviour
{
    [SerializeField] private Image cursor = null;
    [SerializeField] EventSystem eventSystem = null;
    [SerializeField] private GameObject vsImage = null;
    [SerializeField] private GameObject enemyButton = null;
    private GameObject selectedObj;

    private void Start()
    {
        vsImage.SetActive(false);
        this.UpdateAsObservable()
            .Subscribe(_ =>
                {
                    selectedObj = eventSystem.currentSelectedGameObject.gameObject;
                    cursor.transform.position = selectedObj.transform.position + new Vector3(-330, 0, 0);//やり方変えるかも
                });
    }



    public void VsButton()
    {
        vsImage.SetActive(true);
        EventSystem.current.SetSelectedGameObject(enemyButton);
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
