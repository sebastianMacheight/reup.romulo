using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.characterMovement
{

    public class SpacesRecord : MonoBehaviour
    {
        public List<SpaceJumpPoint> jumpPoints;
        [HideInInspector]
        public bool drawSpacesGizmos = true;

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
    }
}
