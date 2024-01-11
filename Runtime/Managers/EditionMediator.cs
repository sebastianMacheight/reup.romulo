using ReupVirtualTwin.managerInterfaces;
using UnityEngine;
using ReupVirtualTwin.enums;
using System.Collections;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using System;
using System.Collections.Generic;
using ReupVirtualTwin.modelInterfaces;

namespace ReupVirtualTwin.managers
{
    public class EditionMediator : MonoBehaviour, IMediator, IWebMessageReceiver
    {
        private ICharacterRotationManager _characterRotationManager;
        public ICharacterRotationManager characterRotationManager
        {
            set { _characterRotationManager = value; }
        }
        private IEditModeManager _editModeManager;
        public IEditModeManager editModeManager
        {
            set
            {
                _editModeManager = value;
            }
        }
        private ISelectedObjectsManager _selectedObjectsManager;
        public ISelectedObjectsManager selectedObjectsManager { set { _selectedObjectsManager = value; } }

        IWebMessagesSender _webMessageSender;
        public IWebMessagesSender webMessageSender { set { _webMessageSender = value; } }

        public void Notify(Events eventName)
        {
            switch (eventName)
            {
                case Events.transformHandleStartIteraction:
                    _characterRotationManager.allowRotation = false;
                    break;
                case Events.transformHandleStopIteraction:
                    _characterRotationManager.allowRotation = true;
                    break;
                default:
                    throw new ArgumentException($"no implementation without payload for event: {eventName}");
            }
        }
        public void Notify<T>(Events eventName, T payload)
        {
            switch(eventName)
            {
                case Events.setEditMode:
                    if (!(payload is bool))
                    {
                        throw new ArgumentException($"Payload must be a boolean for {eventName} events", nameof(payload));
                    }
                    ProccessEditMode((bool)(object)payload);
                    break;
                case Events.setSelectedObjects:
                    if (!(payload is List<GameObject>))
                    {
                        throw new ArgumentException($"Payload must be a List<GameObject> for {eventName} events", nameof(payload));
                    }
                    ProcessSelectedObjects((List<GameObject>)(object)payload);

                    break;
                default:
                    throw new ArgumentException($"no implementation for event: {eventName}");
            }
        }

        public void ReceiveWebMessage(string serializedWebMessage)
        {
            WebMessage<bool> message = JsonUtility.FromJson<WebMessage<bool>>(serializedWebMessage);
            switch (message.type)
            {
                case WebOperationsEnum.setEditMode:
                    _editModeManager.editMode = message.payload;
                    break;
                default:
                    _webMessageSender.SendWebMessage( new WebMessage<string>
                    {
                        type= WebOperationsEnum.error,
                        payload = $"{message.type} not supported",
                    });
                    break;
            }
        }

        private void ProccessEditMode(bool editMode)
        {
            _selectedObjectsManager.allowSelection = editMode;
            if (editMode == false)
            {
                _selectedObjectsManager.ClearSelection();
            }
            WebMessage<bool> message = new WebMessage<bool>
            {
                type = WebOperationsEnum.setEditModeSuccess,
                payload = editMode,
            };
            _webMessageSender.SendWebMessage(message);
        }
        private void ProcessSelectedObjects(List<GameObject> selectedObjects)
        {
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
    }
}
