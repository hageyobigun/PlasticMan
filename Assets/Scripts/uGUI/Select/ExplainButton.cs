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

        //左へ行くボタン（存在しなかったら動かない）
        leftButton.OnClickAsObservable()
            .Where(_ => listNumebr > 0)
            .Subscribe(_ => ChanegeExplainImage(-1));

        //右へ行くボタン（存在しなかったら動かない）
        rightButton.OnClickAsObservable()
            .Where(_ => listNumebr < explainImageList.Count - 1)
            .Subscribe(_ => ChanegeExplainImage(1));
    }

    private void ChanegeExplainImage(int changeValue)
    {
        explainImageList[listNumebr].SetActive(false);//開いているページ閉じる
        listNumebr = listNumebr + changeValue;
        explainImageList[listNumebr].SetActive(true);//次のページ開く
        SoundManager.Instance.PlaySe("NormalButton");
    }
}
