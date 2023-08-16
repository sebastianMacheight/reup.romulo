using ReUpVirtualTwin.Helpers;
using UnityEngine;

namespace ReupVirtualTwin
{
    public class SpaceObjectWithGizmo : MonoBehaviour
    {
        protected SpacesManager _spacesManager;
        protected void CheckSpaceManager()
        {
            if (_spacesManager == null)
                _spacesManager = ObjectFinder.FindSpacesManager();
        }
    }
}
