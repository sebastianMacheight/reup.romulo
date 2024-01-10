using UnityEngine;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface ISelectedObjectsManager
    {
        public bool allowSelection { get;  set; }
        public GameObject selection { get; }
        public void ClearSelection();
        public GameObject AddObjectToSelection(GameObject selectedObject);
        public GameObject RemoveObjectFromSelection(GameObject selectedObject);
    }
}
