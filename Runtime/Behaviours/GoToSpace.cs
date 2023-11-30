using UnityEngine;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.characterMovement;
using UnityEngine.Events;
using System;

namespace ReupVirtualTwin.behaviours
{
    public class GoToSpace : MonoBehaviour
    {
        CharacterPositionManager _characterPositionManager;
        SpaceJumpPoint spaceSelector;
        private void Start()
        {
            _characterPositionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
            spaceSelector = GetComponent<SpaceButtonInstance>().spaceSelector;
        }

        public void Go()
        {
            _characterPositionManager.MakeKinematic();
            var spaceSelectorPosition = spaceSelector.transform.position;
            spaceSelectorPosition.y = GetDesiredHeight();
            var endMovementEvent = new UnityEvent();
            endMovementEvent.AddListener(EndMovementHandler);
            _characterPositionManager.allowWalking = false;
            _characterPositionManager.allowSetHeight = false;
            _characterPositionManager.SlideToTarget(spaceSelectorPosition,endMovementEvent);
        }

        private void EndMovementHandler()
        {
            _characterPositionManager.UndoKinematic();
            _characterPositionManager.allowWalking = true;
            _characterPositionManager.allowSetHeight = true;
        }

        private float GetDesiredHeight()
        {
            var groundHit = GetGroundHit();
            if (groundHit == null)
            {
                throw new Exception("No Ground below Space selector");
            }
            return MaintainHeight.GetDesiredHeightInGround(((RaycastHit)groundHit).point.y);
        }

        private RaycastHit? GetGroundHit()
        {
            RaycastHit hit;
            var ray = GetDownRayFromSpaceSelector();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                return hit;
            }
            return null;
        }
        private Ray GetDownRayFromSpaceSelector()
        {
            return new Ray(spaceSelector.transform.position, Vector3.down);
        }
    }
}
