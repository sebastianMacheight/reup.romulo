using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManager : MonoBehaviour, ISelectedObjectsManager
    {
        private IObjectWrapper _objectWrapper;
        public IObjectWrapper objectWrapper { set =>  _objectWrapper = value; }
        private IObjectHighlighter _highlighter;
        public IObjectHighlighter highlighter { set => _highlighter = value; }
        private IMediator _mediator;
        public IMediator mediator { set { _mediator = value; } }
        private GameObject _selection;
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
                _mediator.Notify(Events.setSelectedObjects, new ObjectWrapperDTO
                {
                    wrapper = _selection,
                    wrappedObjects = _objectWrapper.wrappedObjects,
                });
            }
        }

        private bool _allowSelection = false;
        public bool allowSelection { get => _allowSelection; set
            {
                _allowSelection = value;
                if (!_allowSelection)
                {
                    ClearSelection();
                }
            }
        }


        public GameObject AddObjectToSelection(GameObject selectedObject)
        {
            if (!_allowSelection) return null;
            selection = _objectWrapper.WrapObject(selectedObject);
            return _selection;
        }

        public void ClearSelection()
        {
            if (_objectWrapper.wrappedObjects.Count > 0)
            {
                _objectWrapper.DeWrapAll();
            }
            if (_selection != null)
            {
                selection = null;
            }
        }

        public GameObject RemoveObjectFromSelection(GameObject selectedObject)
        {
            selection = _objectWrapper.UnwrapObject(selectedObject);
            return _selection;
        }
    }
}
