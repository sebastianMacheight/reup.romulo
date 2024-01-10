using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.behaviourInterfaces;
using System.Collections.Generic;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.enums;
using UnityEngine.UIElements;
using System;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManager : MonoBehaviour, ISelectedObjectsManager
    {
        private IObjectWrapper _objectWrapper;
        private IObjectHighlighter _highlighter;
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
                _mediator.Notify(Events.setSelectedObjects, _objectWrapper.wrappedObjects);
            }
        }

        private bool _allowSelection = false;
        public bool allowSelection { get => _allowSelection; set
            {
                ClearSelection();
                _allowSelection = value;
            }
        }

        private void Start()
        {
            _objectWrapper = new ObjectWrapper();
            _highlighter = new Outliner();
        }

        public GameObject AddObjectToSelection(GameObject selectedObject)
        {
            if (!_allowSelection) return null;
            selection = _objectWrapper.WrapObject(selectedObject);
            return _selection;
        }

        public void ClearSelection()
        {
            if (!_allowSelection) return;
            _objectWrapper.DeWrapAll();
            selection = null;
        }

        public GameObject RemoveObjectFromSelection(GameObject selectedObject)
        {
            if (!_allowSelection) return null;
            selection = _objectWrapper.DeWrapObject(selectedObject);
            return _selection;
        }
    }
}
