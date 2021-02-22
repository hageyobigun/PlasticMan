using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCursor : MonoBehaviour
{
    [SerializeField] private Image cursorImage = default;
    [SerializeField] private EventSystem eventSystem = default;

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var selectedObj = eventSystem.currentSelectedGameObject.gameObject;

        cursorImage.transform.position = selectedObj.transform.position;
        rectTransform = selectedObj.GetComponent<RectTransform>();
        cursorImage.rectTransform.sizeDelta = rectTransform.sizeDelta + new Vector2(20, 20);
    }
}
