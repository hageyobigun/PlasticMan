using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;


public class ExplainButton : MonoBehaviour
{
    [SerializeField] private Button leftButton = null;
    [SerializeField] private Button rightButton = null;

    [SerializeField] private List<GameObject> explainImageList = new List<GameObject>();


    private int listNumebr;

    // Start is called before the first frame update
    void Awake()
    {
        listNumebr = 0;

        leftButton.OnClickAsObservable()
            .Where(_ => listNumebr > 0)
            .Subscribe(_ =>
            {
                ChanegeExplainImage(-1);
            });

        rightButton.OnClickAsObservable()
            .Where(_ => listNumebr < explainImageList.Count - 1)
            .Subscribe(_ =>
            {
                ChanegeExplainImage(1);
            });
    }

    private void ChanegeExplainImage(int changeValue)
    {
        explainImageList[listNumebr].SetActive(false);
        listNumebr = listNumebr + changeValue;
        explainImageList[listNumebr].SetActive(true);
        SoundManager.Instance.PlaySe("NormalButton");
    }
}
