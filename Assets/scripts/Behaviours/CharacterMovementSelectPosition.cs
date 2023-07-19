using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSelectPosition : SelectPoint
{
    CharacterPositionManager _characterPositionManager;
    private void Start()
    {
        _characterPositionManager = GetComponent<CharacterPositionManager>();
    }
    public override void HandleHit(RaycastHit hit)
    {
        _characterPositionManager.MovePositionToTarget(hit.point);
    }
    public override void MissHit()
    {
        //Debug.Log("No hit for movement");
    }
}
