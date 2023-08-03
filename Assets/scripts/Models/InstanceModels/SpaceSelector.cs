using ReUpVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSelector : MonoBehaviour
{
    public string spaceName = "Unnamed space";

    SpacesManager _spacesManager;

    private void OnDrawGizmos()
    {
        if (_spacesManager == null)
            _spacesManager = ObjectFinder.FindSpacesManager();
        if (_spacesManager.drawSpacesGizmos)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}
