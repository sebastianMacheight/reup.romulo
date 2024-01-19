using ReupVirtualTwin.models;
using UnityEngine;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.controllerInterfaces;

namespace ReupVirtualTwin.controllers
{
    public class IdController : IIdGetterController, IIdAssignerController
    {
        public void AssignIdsToTree(GameObject tree)
        {
            AssignIdToObject(tree);
            foreach (Transform child in tree.transform)
            {
                AssignIdsToTree(child.gameObject);
            }
        }

        public IUniqueIdentifer AssignIdToObject(GameObject obj)
        {
            IUniqueIdentifer uniqueId = obj.GetComponent<IUniqueIdentifer>();
            if (uniqueId == null)
            {
                uniqueId = obj.AddComponent<RegisteredIdentifier>();
                uniqueId.GenerateId();
            }
            return uniqueId;
        }

        public void RemoveIdsFromTree(GameObject tree)
        {
            RemoveIdFromObject(tree);
            foreach (Transform child in tree.transform)
            {
                RemoveIdsFromTree(child.gameObject);
            }
        }

        public void RemoveIdFromObject(GameObject obj)
        {
            IUniqueIdentifer identifier = obj.GetComponent<IUniqueIdentifer>();
            if (identifier != null)
            {
                Object.DestroyImmediate((Object)identifier);
            }
        }

        public string GetIdFromObject(GameObject obj)
        {
            return obj.GetComponent<IUniqueIdentifer>().getId();
        }
    }
}
