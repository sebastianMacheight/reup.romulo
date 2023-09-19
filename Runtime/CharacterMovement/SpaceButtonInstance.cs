using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.characterMovement
{
    public class SpaceButtonInstance : MonoBehaviour
    {
        public SpaceJumpPoint spaceSelector;

        CharacterPositionManager _characterPositionManager;
        SpacesRecord _spacesRecord;
        public string spaceName
        {
            get { return nameField.text; }
            set { nameField.text = value; }
        }

        [SerializeField]
        TMP_Text nameField;

        private void Start()
        {
            _characterPositionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
            _spacesRecord = ObjectFinder.FindSpacesRecord().GetComponent<SpacesRecord>();
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
            foreach (var plane in _spacesRecord.floorPlanes)
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
}
