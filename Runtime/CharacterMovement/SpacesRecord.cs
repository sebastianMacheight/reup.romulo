using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.characterMovement
{

    public class SpacesRecord : MonoBehaviour
    {
        public List<SpaceJumpPoint> jumpPoints;
        public List<FloorPlane> floorPlanes;
        [HideInInspector]
        public bool drawSpacesGizmos = true;

        void Start()
        {
            UpdatePlanes();
        }

        public void UpdateSpaces()
        {
            GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
            jumpPoints.Clear();
            foreach (GameObject room in spaces)
            {
                var roomSelector = room.GetComponent<SpaceJumpPoint>();
                jumpPoints.Add(roomSelector);
            }
        }
        public void UpdatePlanes()
        {
            GameObject[] planes = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelectorPlane);
            floorPlanes.Clear();
            foreach (GameObject plane in planes)
            {
                var spaceSelectorPlane = plane.GetComponent<FloorPlane>();
                floorPlanes.Add(spaceSelectorPlane);
            }
        }
    }
}
