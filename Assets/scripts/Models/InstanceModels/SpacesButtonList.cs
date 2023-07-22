using ReUpVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacesButtonList : MonoBehaviour
{
    public GameObject spaceButtonPrefab;

    IObjectPool objectPool;
    SpacesManager spacesManager;
   

    private void Start()
    {
        objectPool = ObjectFinder.FindObjectPool();
        spacesManager = ObjectFinder.FindSpacesManager();

        foreach(SpaceSelector space in spacesManager.spaceSelectors)
        {
            GameObject spaceButton = objectPool.GetObjectFromPool(spaceButtonPrefab.name, transform);
            var spaceButtonInstance = spaceButton.GetComponent<SpaceButtonInstance>();
            spaceButtonInstance.spaceSelector = space;
            spaceButtonInstance.spaceName = space.spaceName;
        }
    }
}
