﻿using UnityEngine;
using UnityEngine.UI;

public class SelectCursor
{
    private Image cursorImage = default;

    public SelectCursor(Image _cursorImage)
    {
        cursorImage = _cursorImage;
    }

    public void CursorMove(GameObject selectObj)
    {
        var rectTransform = selectObj.GetComponent<RectTransform>();
        cursorImage.rectTransform.sizeDelta = rectTransform.sizeDelta + new Vector2(20, 20); //ボタンの大きさmに合わせる
        cursorImage.transform.position = selectObj.transform.position; //位置を合わせる
    }
}
