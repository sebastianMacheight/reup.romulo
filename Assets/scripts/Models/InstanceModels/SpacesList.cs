using Codice.Client.Common.FsNodeReaders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacesList : MonoBehaviour
{
    [SerializeField]
    float maxHeight = 400;

    public void FixHeight(float height)
    {
        if (height > maxHeight) return;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }

}
