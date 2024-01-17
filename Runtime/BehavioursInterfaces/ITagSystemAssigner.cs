using UnityEngine;

namespace ReupVirtualTwin.behaviourInterfaces
{
    public interface ITagSystemAssigner
    {
        public void AssignTagSystemToTree(GameObject tree);
        public void RemoveTagSystemFromTree(GameObject tree);
    }
}
