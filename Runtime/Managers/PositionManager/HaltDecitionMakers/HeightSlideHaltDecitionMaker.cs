using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class HeightSlideHaltDecitionMaker : MovementHaltDecitionMaker<float>
    {
        CharacterPositionManager characterPositionManager;
        float stopMovementThreshold;

        public HeightSlideHaltDecitionMaker(CharacterPositionManager cpm, float stopThreshold)
        {
            characterPositionManager = cpm;
            stopMovementThreshold = stopThreshold;
        }
        public bool ShouldKeepMoving(float target)
        {
            float distanceToHeight = Mathf.Abs(target - characterPositionManager.characterPosition.y);
            bool thereIsDistanceToTarget = distanceToHeight > stopMovementThreshold;
            return thereIsDistanceToTarget;
        }

    }
}
