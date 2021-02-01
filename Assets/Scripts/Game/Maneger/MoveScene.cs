using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MoveScene
{

    private List<GameObject> backList = new List<GameObject>();

    public void GetBackList(GameObject canvas, bool isClose)
    {
        // 子オブジェクトを全て取得する
        foreach (Transform childTransform in canvas.transform)
        {
            backList.Add(childTransform.gameObject);
            childTransform.gameObject.SetActive(isClose);
        }
    }

    public IEnumerator OpenBlackBlock(GameObject canvas)
    {
        GetBackList(canvas, false);
        while (backList.Count > 0)
        {
            int index = Random.Range(0, backList.Count);
            backList[index].SetActive(true);
            backList.RemoveAt(index);
            if (backList.Count % 2 == 0)
            {
                yield return new WaitForSeconds(1.0f / 100000);
            }
        }
        yield return new WaitForSeconds(0.6f);
    }

    public IEnumerator CloseBlackBlock(GameObject canvas)
    {
        GetBackList(canvas, true);
        while (backList.Count > 0)
        {
            int index = Random.Range(0, backList.Count);
            backList[index].SetActive(false);
            backList.RemoveAt(index);
            if (backList.Count % 2 == 0)
            {
                yield return new WaitForSeconds(1.0f / 100000);
            }
        }
        yield return new WaitForSeconds(0.6f);
    }
}
