using UnityEngine;
using ReupVirtualTwin.characterMovement;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.behaviours
{

    public class CharacterMovementSelectPosition : SelectPoint
    {
        CharacterPositionManager _characterPositionManager;

        protected override void Start()
        {
            base.Start();
            _characterPositionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
        }

        public override void HandleHit(RaycastHit hit)
        {
            if (_characterPositionManager.allowWalking)
            {
                _characterPositionManager.WalkToTarget(hit.point);
            }
        }
    }
}
