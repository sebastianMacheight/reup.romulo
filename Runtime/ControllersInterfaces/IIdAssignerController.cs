using ReupVirtualTwin.modelInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.controllerInterfaces
{
    public interface IIdAssignerController
    {
        public void AssignIdsToTree(GameObject tree);
        public void RemoveIdsFromTree(GameObject tree);
        public IUniqueIdentifer AssignIdToObject(GameObject obj);
        public void RemoveIdFromObject(GameObject obj);
    }
}
