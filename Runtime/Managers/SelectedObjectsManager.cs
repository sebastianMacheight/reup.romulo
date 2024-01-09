using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.helperInterfaces;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManager : MonoBehaviour, ISelectedObjectsManager
    {
        private IObjectWrapper _objectWrapper;
        private IObjectHighlighter _highlighter;
        GameObject _selection;
        public GameObject selection
        {
            get => _selection;
            set
            {
                if (_selection != null)
                {
                    _highlighter.UnhighlightObject(_selection);
                }
                _selection = value;
                if (_selection != null)
                {
                    _highlighter.HighlightObject(_selection);
                }
            }
        }

        private void Start()
        {
            _objectWrapper = new ObjectWrapper();
            _highlighter = new Outliner();
        }

        public GameObject AddObjectToSelection(GameObject selectedObject)
        {
            selection = _objectWrapper.WrapObject(selectedObject);
            return _selection;
        }

        public void ClearSelection()
        {
            selection = null;
            _objectWrapper.DeWrapAll();
        }

        public GameObject RemoveObjectFromSelection(GameObject selectedObject)
        {
            selection = _objectWrapper.DeWrapObject(selectedObject);
            return _selection;
        }
    }
}
