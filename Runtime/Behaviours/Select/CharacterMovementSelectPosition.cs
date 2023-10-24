using UnityEngine;
using ReupVirtualTwin.characterMovement;

public class CharacterMovementSelectPosition : SelectPoint
{
    [SerializeField]
    CharacterPositionManager _characterPositionManager;

    public override void HandleHit(RaycastHit hit)
    {
        _characterPositionManager.WalkToTarget(hit.point);
    }
}
