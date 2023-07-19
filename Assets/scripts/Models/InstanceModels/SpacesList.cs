using ReUpVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacesList : MonoBehaviour
{
    public GameObject spaceButtonPrefab;

    SpacesManager spacesManager;
    IObjectPool objectPool;

    void Start()
    {
        spacesManager = ObjectFinder.FindSpacesManager();
        objectPool = ObjectFinder.FindObjectPool();

        Debug.Log(spacesManager);
        //Debug.Log(spacesManager.registeredRooms);
        //foreach(SpaceSelector spaceSelector in spacesManager.registeredRooms.roomSelectors)
        //{
        //    Debug.Log(spaceSelector.spaceName);
        //    GameObject spaceButton = objectPool.GetObjectFromPool(spaceButtonPrefab.name, transform);
        //    spaceButton.GetComponent<SpaceButtonInstance>().nameField.text = spaceSelector.spaceName;
        //}
    }
}
