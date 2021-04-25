using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene
{

    private List<GameObject> blackImageList = new List<GameObject>();//たくさんの黒い正方形を収納
    private float intervalTime = 0.00001f;
    private float fullImageInterval = 0.5f;
    private int openPanelCount = 2;//一回で出すパネルの数

    public void GetSceneImage(GameObject canvas, bool isLoad)
    {
        // 子オブジェクトを全て取得し、見えなくする
        foreach (Transform childTransform in canvas.transform)
        {
            blackImageList.Add(childTransform.gameObject);
            childTransform.gameObject.SetActive(isLoad);
        }
    }

    //シーン移動演出
    public IEnumerator LoadSceneImage(GameObject canvas, bool isLoad)
    {
        GetSceneImage(canvas, !isLoad);
        //表示を切り替えたらリストからisLoadによって出したり消したりする
        while (blackImageList.Count > 0)
        {
            int index = Random.Range(0, blackImageList.Count);
            blackImageList[index].SetActive(isLoad);
            blackImageList.RemoveAt(index);
            if (blackImageList.Count % openPanelCount == 0)//n個ずつ
            {
                yield return new WaitForSeconds(intervalTime);
            }
        }
        yield return new WaitForSeconds(fullImageInterval);
    }
}
