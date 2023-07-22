using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacesManager : MonoBehaviour
{
    public List<SpaceSelector> spaceSelectors;
    void Start()
    {
        UpdateSpaces();
        Debug.Log($"in the spacesmanager there are {spaceSelectors.Count}");

        //foreach( SpaceSelector selector in spaceSelectors )
        //{
        //    Debug.Log($"the space is {selector}");
        //}
    }

    public void UpdateSpaces()
    {
        GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
        spaceSelectors.Clear();
        foreach (GameObject room in spaces)
        {
            var roomSelector = room.GetComponent<SpaceSelector>();
            spaceSelectors.Add(roomSelector);
        }
    }
}
