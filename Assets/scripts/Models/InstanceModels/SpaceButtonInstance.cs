using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ReUpVirtualTwin.Helpers;

public class SpaceButtonInstance : MonoBehaviour
{
    public SpaceJumpPoint spaceSelector;

    CharacterPositionManager _characterPositionManager;
    SpacesManager _spacesManager;
    public string spaceName
    {
        get { return nameField.text; }
        set { nameField.text = value; }
    }

    [SerializeField]
    TMP_Text nameField;

    private void Start()
    {
        _characterPositionManager = ObjectFinder.FindCharacterPositionManager();
        _spacesManager = ObjectFinder.FindSpacesManager();
    }

    public void GoToSpace()
    {
        var spaceSelectorPosition = spaceSelector.transform.position;
        var floorPlane = GetClosestFloorPlane(spaceSelectorPosition);
        var newPosition = spaceSelectorPosition;
        newPosition.y = floorPlane.gameObject.transform.position.y;
        _characterPositionManager.SliceToTarget(newPosition);
    }

    FloorPlane GetClosestFloorPlane(Vector3 position)
    {
        var height = position.y;
        float minDistance = Mathf.Infinity;
        FloorPlane closestPlane = null;
        float distance;
        foreach(var plane in _spacesManager.floorPlanes)
        {
            if ((distance = Mathf.Abs(height - plane.gameObject.transform.position.y)) < minDistance)
            {
                closestPlane = plane;
                minDistance = distance;
            }
        }
        return closestPlane;
    }
}
