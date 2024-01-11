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
        private ITransformSelectedManager _transformSelectedManager;
        public ITransformSelectedManager transformSelectedManager { set => _transformSelectedManager = value; }

        IWebMessagesSender _webMessageSender;
        public IWebMessagesSender webMessageSender { set { _webMessageSender = value; } }

        public void Notify(Events eventName)
        {
            switch (eventName)
            {
                case Events.transformHandleStartItneraction:
                    _characterRotationManager.allowRotation = false;
                    break;
                case Events.transformHandleStopInteraction:
                    _characterRotationManager.allowRotation = true;
                    break;
                case Events.rotationTransformModeActivated:
                    ProcessTranformModeActivation(TransformMode.RotationMode);
                    break;
                case Events.positionTransformModeActivated:
                    ProcessTranformModeActivation(TransformMode.PositionMode);
                    break;
                case Events.transformModeDeactivated:
                    ProcessTranformModeDeactivation();
                    break;
                default:
                    throw new ArgumentException($"no implementation without payload for event: {eventName}");
            }
        }
        public void Notify<T>(Events eventName, T payload)
        {
            switch (eventName)
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
                case WebMessageType.setEditMode:
                    _editModeManager.editMode = message.payload;
                    break;
                case WebMessageType.activatePositionTransform:
                    _transformSelectedManager.ActivateTransformMode(_selectedObjectsManager.selection, TransformMode.PositionMode);
                    break;
                case WebMessageType.activateRotationTransform:
                    _transformSelectedManager.ActivateTransformMode(_selectedObjectsManager.selection, TransformMode.PositionMode);
                    break;
                case WebMessageType.deactivateTransformMode:
                    _transformSelectedManager.DeactivateTransformMode();
                    break;
                default:
                    _webMessageSender.SendWebMessage(new WebMessage<string>
                    {
                        type = WebMessageType.error,
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
                type = WebMessageType.setEditModeSuccess,
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
                type = WebMessageType.setSelectedObjects,
                payload = objectDTOs
            };
            _webMessageSender.SendWebMessage(message);
        }
        private void ProcessTranformModeActivation(TransformMode mode)
        {
            string eventName;
            if (mode == TransformMode.PositionMode)
            {
                eventName = WebMessageType.activatePositionTransformSuccess;
            }
            else if (mode == TransformMode.RotationMode)
            {
                eventName = WebMessageType.activateRotationTransformSuccess;
            }
            else
            {
                throw new Exception($"unnown TransformMode {mode}");
            }
            WebMessage<string> message = new WebMessage<string>
            {
                type = eventName,
            };
            _webMessageSender.SendWebMessage(message);
        }
        private void ProcessTranformModeDeactivation()
        {
            WebMessage<string> message = new WebMessage<string>
            {
                type = WebMessageType.deactivateTransformModeSuccess,
            };
            _webMessageSender.SendWebMessage(message);
        }
    }
}
