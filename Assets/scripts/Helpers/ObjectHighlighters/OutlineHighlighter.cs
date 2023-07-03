using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHighlighter : MonoBehaviour, IHighligher
{
    private Color outlineColor = Color.yellow;
    private float outlineWidth = 10f;

    public void AddSelection(GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<Outline>() == null)
        {
            var outline = obj.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = outlineWidth;
        }
    }

    public void RemoveSelection(GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<Outline>() != null)
        {
            Destroy(obj.GetComponent<Outline>());
        }
    }
}
