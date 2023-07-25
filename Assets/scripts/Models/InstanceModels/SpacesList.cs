using UnityEngine;

public class SpacesList : MonoBehaviour
{
    [SerializeField]
    float maxHeight = 400;

    public void FixHeight(float height)
    {
        //if (height > maxHeight) return;
        Debug.Log($"fixing height for {height}");
        var rectTransform = GetComponent<RectTransform>();
        Debug.Log($"originally is widht: {rectTransform.sizeDelta.x} height: {rectTransform.sizeDelta.y}");
        Debug.Log($"originally is widht: {rectTransform.rect.width} height: {rectTransform.rect.height}");
        //Since the vertical anchors of the spaceList are attached to the corners of the parent
        //the sizedelta attribute is how much bigger or smaller the rectangle is compared to its parent
        //so that's why this calculation
        var newSizeDeltaY = -(rectTransform.rect.height - height) + rectTransform.sizeDelta.y;
        Debug.Log($"the newsizedeltay is {newSizeDeltaY}");
        if (newSizeDeltaY < 0)
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newSizeDeltaY);

    }

}
