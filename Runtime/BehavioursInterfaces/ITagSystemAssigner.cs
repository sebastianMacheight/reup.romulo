using ReupVirtualTwin.modelInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.behaviourInterfaces
{
    public interface ITagSystemAssigner
    {
        public void AssignTagSystemToTree(GameObject tree);
        public IObjectTags AssignTagSystemToObject(GameObject obj);
        public void RemoveTagSystemFromObject(GameObject obj);
        public void RemoveTagSystemFromTree(GameObject tree);
    }
}
