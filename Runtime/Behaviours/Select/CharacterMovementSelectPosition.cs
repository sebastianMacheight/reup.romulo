using UnityEngine;
using ReupVirtualTwin.characterMovement;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.behaviours
{

    public class CharacterMovementSelectPosition : SelectPoint
    {
        CharacterPositionManager _characterPositionManager;

        private void Start()
        {
            _characterPositionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
        }

        public override void HandleHit(RaycastHit hit)
        {
            _characterPositionManager.WalkToTarget(hit.point);
        }
    }
}
