using ReUpVirtualTwin.Helpers;
using ReupVirtualTwinMain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceJumpPoint : SpaceObjectWithGizmo
{
    public string spaceName = "Unnamed space";

    private void OnDrawGizmos()
    {
        CheckSpaceManager();
        if (_spacesManager == null)
            _spacesManager = ObjectFinder.FindSpacesManager();
        if (_spacesManager.drawSpacesGizmos)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}
