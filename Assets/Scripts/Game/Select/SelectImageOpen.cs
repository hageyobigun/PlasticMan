using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectImageOpen
{
    
    public IEnumerator OpenImage(GameObject openImage)
    {
        var size = 0f;
        var speed = 0.05f;
        var maxScale = openImage.transform.lossyScale;

        while (size <= maxScale.x)
        {
            openImage.transform.localScale = new Vector3(size, maxScale.y, maxScale.z);
            size += speed;

            yield return null;
        }
    }
}
