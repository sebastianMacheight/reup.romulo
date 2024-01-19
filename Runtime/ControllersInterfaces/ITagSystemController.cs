using UnityEngine;

namespace ReupVirtualTwin.controllerInterfaces
{
    public interface ITagSystemController
    {
        public void AssignTagSystemToTree(GameObject tree);
        public void RemoveTagSystemFromTree(GameObject tree);
    }
}
