using ReUpVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacesButtonList : MonoBehaviour
{
    public GameObject spaceButtonPrefab;
    public SpacesList spacesList;

    IObjectPool objectPool;
    SpacesManager spacesManager;
   

    private void Start()
    {
        objectPool = ObjectFinder.FindObjectPool();
        spacesManager = ObjectFinder.FindSpacesManager();

        foreach(SpaceJumpPoint space in spacesManager.jumpPoints)
        {
            GameObject spaceButton = objectPool.GetObjectFromPool(spaceButtonPrefab.name, transform);
            var spaceButtonInstance = spaceButton.GetComponent<SpaceButtonInstance>();
            spaceButtonInstance.spaceSelector = space;
            spaceButtonInstance.spaceName = space.spaceName;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);

        // Update the size of the parent panel to fit the new content
        RectTransform parentRectTransform = (RectTransform)transform;
        float newHeight = GetComponent<VerticalLayoutGroup>().preferredHeight;
        parentRectTransform.sizeDelta = new Vector2(parentRectTransform.sizeDelta.x, newHeight);
        spacesList.FixHeight(newHeight + 60);
    }
}
