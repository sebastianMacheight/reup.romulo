using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManager : MonoBehaviour, ISelectedObjectsManager
    {
        private IObjectWrapper _objectWrapper;
        GameObject _selection;
        public GameObject selection { get => _selection; }

        private void Start()
        {
            _objectWrapper = new ObjectWrapper();
        }

        public GameObject AddObjectToSelection(GameObject selectedObject)
        {
            _selection = _objectWrapper.WrapObject(selectedObject);
            return _selection;
        }

        public void ClearSelection()
        {
            _objectWrapper.DeWrapAll();
            _selection = null;
        }

        public GameObject RemoveObjectFromSelection(GameObject selectedObject)
        {
            _selection = _objectWrapper.DeWrapObject(selectedObject);
            return _selection;
        }
    }
}
