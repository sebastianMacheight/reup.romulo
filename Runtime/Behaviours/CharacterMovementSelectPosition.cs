using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ReupVirtualTwin.characterMovement;

public class CharacterMovementSelectPosition : SelectPoint
{
    CharacterPositionManager _characterPositionManager;
    protected override void Start()
    {
        base.Start();
        _characterPositionManager = GetComponent<CharacterPositionManager>();
    }
    public override void HandleHit(RaycastHit hit)
    {
        _characterPositionManager.WalkToTarget(hit.point);
    }
}
