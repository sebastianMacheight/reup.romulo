using UnityEngine;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface ISelectedObjectsManager
    {
        public GameObject selection { get; }
        public void ClearSelection();
        public GameObject AddObjectToSelection(GameObject selectedObject);
        public GameObject RemoveObjectFromSelection(GameObject selectedObject);
    }
}
