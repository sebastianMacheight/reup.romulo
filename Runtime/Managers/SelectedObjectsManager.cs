using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.behaviourInterfaces;
using System.Collections.Generic;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManager : MonoBehaviour, ISelectedObjectsManager
    {
        private IObjectWrapper _objectWrapper;
        private IObjectHighlighter _highlighter;
        private IWebMessagesSender _webMessageSender;
        private GameObject _selection;
        public IWebMessagesSender webMessageSender { set { _webMessageSender = value; } }
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
                SendSetSelectedObjectsMessage();
            }
        }

        private void SendSetSelectedObjectsMessage()
        {
            if (_selection == null)
            {
                _webMessageSender.SendWebMessage(new WebMessage<string>
                {
                    type = WebOperationsEnum.setSelectedObjects,
                    payload = "[]"
                });
                return;
            }
            List<GameObject> selectedObjects = _objectWrapper.wrappedObjects;
            List<ObjectDTO> selectedDTOObjects = new List<ObjectDTO>();
            foreach (GameObject obj in selectedObjects)
            {
                string objId = obj.GetComponent<IUniqueIdentifer>().getId();
                selectedDTOObjects.Add(new ObjectDTO { objectId = objId });
            }
            ObjectDTO[] objectDTOs = selectedDTOObjects.ToArray();
            WebMessage<ObjectDTO[]> message = new WebMessage<ObjectDTO[]>
            {
                type = WebOperationsEnum.setSelectedObjects,
                payload = objectDTOs
            };
            _webMessageSender.SendWebMessage(message);
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
