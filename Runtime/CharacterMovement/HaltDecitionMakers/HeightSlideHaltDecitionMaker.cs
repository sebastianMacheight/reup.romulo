using UnityEngine;

namespace ReupVirtualTwin.characterMovement
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
            if (thereIsDistanceToTarget)
            {
                Debug.Log($"current height: {characterPositionManager.characterPosition.y}");
                Debug.Log($"current target: {target}");
            }
            return thereIsDistanceToTarget;
        }

    }
}
