using UnityEngine;
using UnityEngine.UI;

public class SelectCursor
{
    private Image cursorImage = default;
    private Vector2 size = new Vector2(20, 20);

    public SelectCursor(Image _cursorImage)
    {
        cursorImage = _cursorImage;
    }

    public void CursorMove(GameObject selectObj)
    {
        var rectTransform = selectObj.GetComponent<RectTransform>();
        cursorImage.rectTransform.sizeDelta = rectTransform.sizeDelta + size; //ボタンの大きさ  + sizeに合わせる
        cursorImage.transform.position = selectObj.transform.position; //位置を合わせる
    }
}
