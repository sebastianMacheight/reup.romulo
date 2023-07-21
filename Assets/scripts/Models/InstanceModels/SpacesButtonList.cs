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
        Debug.Log("the spaces are: ");

        foreach(SpaceSelector space in spacesManager.spaceSelectors)
        {
            var spaceButton = objectPool.GetObjectFromPool(spaceButtonPrefab.name, transform);
            Debug.Log(space.spaceName);
            spaceButton.GetComponent<SpaceButtonInstance>().spaceName = space.spaceName;
        }
    }
}
