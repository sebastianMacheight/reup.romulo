using UnityEngine;
using ReupVirtualTwin.characterMovement;
using ReupVirtualTwin.managerInterfaces;

namespace ReupVirtualTwin.behaviours
{
    public class CharacterMovementSelectPosition : SelectPoint
    {
        private IEditModeManager _editModeManager;
        private CharacterPositionManager _characterPositionManager;

        public override void HandleHit(RaycastHit hit)
        {
            if (_characterPositionManager.allowWalking && _editModeManager.editMode == false)
            {
                _characterPositionManager.WalkToTarget(hit.point);
            }
        }
        public IEditModeManager editModeManager
        {
            set { _editModeManager = value; }
        }
        public CharacterPositionManager characterPositionManager
        {
            set { _characterPositionManager = value; }
        }
    }
}
