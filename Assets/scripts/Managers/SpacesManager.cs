using System.Collections.Generic;
using UnityEngine;

public class SpacesManager : MonoBehaviour
{
    public List<SpaceJumpPoint> spaceSelectors;
    public List<FloorPlane> spaceSelectorPlanes;
    [HideInInspector]
    public bool drawSpacesGizmos = true;
    [HideInInspector]
    public bool drawPlanesGizmos = true;
    [HideInInspector]
    public int floorPlanesLength = 20;
    [HideInInspector]
    public int gizmoGridSize = 9;

    void Start()
    {
        UpdateSpaces();
        UpdatePlanes();
    }

    public void UpdateSpaces()
    {
        GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
        spaceSelectors.Clear();
        foreach (GameObject room in spaces)
        {
            var roomSelector = room.GetComponent<SpaceJumpPoint>();
            spaceSelectors.Add(roomSelector);
        }
    }
    public void UpdatePlanes()
    {
        GameObject[] planes = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelectorPlane);
        spaceSelectorPlanes.Clear();
        foreach (GameObject plane in planes)
        {
            var spaceSelectorPlane = plane.GetComponent<FloorPlane>();
            spaceSelectorPlanes.Add(spaceSelectorPlane);
        }
    }
}
