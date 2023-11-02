using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.characterMovement
{

    public class SpacesRecord : MonoBehaviour
    {
        public List<SpaceJumpPoint> jumpPoints;
        [HideInInspector]
        bool _drawSpacesGizmos = true;
        public bool drawSpacesGizmos
        {
            get
            {
                return _drawSpacesGizmos;
            }
            set
            {
                _drawSpacesGizmos = value;
                UpdateSpaces();
            }
        }

        public void UpdateSpaces()
        {
            GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
            jumpPoints.Clear();
            foreach (GameObject room in spaces)
            {
                var roomSelector = room.GetComponent<SpaceJumpPoint>();
                roomSelector.drawGizmo = drawSpacesGizmos;
                jumpPoints.Add(roomSelector);
            }
        }
    }
}
