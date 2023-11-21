using UnityEngine;

namespace ReupVirtualTwin.characterMovement
{
    public class SpaceSlideHaltDecitionMaker : MovementHaltDecitionMaker<Vector3>
    {
        CharacterPositionManager characterPositionManager;
        float stopMovementThreshold;

        public SpaceSlideHaltDecitionMaker(CharacterPositionManager cpm, float stopThreshold)
        {
            characterPositionManager = cpm;
            stopMovementThreshold = stopThreshold;
        }

        public bool ShouldKeepMoving(Vector3 target)
        {
            bool thereIsDistanceToTarget = Vector3.Distance(target, characterPositionManager.characterPosition) > stopMovementThreshold;
            return thereIsDistanceToTarget;

        }
    }
}
