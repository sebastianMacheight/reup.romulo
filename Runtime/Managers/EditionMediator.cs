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
                case Events.positionTransformModeActivated:
                    ProcessTransformModeActivation(TransformMode.PositionMode);
                    break;
                case Events.rotationTransformModeActivated:
                    ProcessTransformModeActivation(TransformMode.RotationMode);
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
                    if (!(payload is ObjectWrapperDTO))
                    {
                        throw new ArgumentException($"Payload must be of type {nameof(ObjectWrapperDTO)} for {eventName} events", nameof(payload));
                    }
                    ProcessNewWrapper((ObjectWrapperDTO)(object)payload);
                    break;
                default:
                    throw new ArgumentException($"no implementation for event: {eventName}");
            }
        }

        public void ReceiveWebMessage(string serializedWebMessage)
        {
            try
            {
                WebMessage<string> message = JsonUtility.FromJson<WebMessage<string>>(serializedWebMessage);
                ProcessWebMessage(message);
            }
            catch (RomuloException e)
            {
                _webMessageSender.SendWebMessage(new WebMessage<string>
                {
                    type = WebMessageType.error,
                    payload = e.Message,
                });
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public void ProcessWebMessage(WebMessage<string> message)
        {
            switch (message.type)
            {
                case WebMessageType.setEditMode:
                    _editModeManager.editMode = bool.Parse(message.payload);
                    break;
                case WebMessageType.activatePositionTransform:
                    ActivateTransformMode(TransformMode.PositionMode);
                    break;
                case WebMessageType.activateRotationTransform:
                    ActivateTransformMode(TransformMode.RotationMode);
                    break;
                case WebMessageType.deactivateTransformMode:
                    DeactivateTransformMode();
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

        private void ActivateTransformMode(TransformMode mode)
        {
            if (_selectedObjectsManager.wrapperDTO == null || _selectedObjectsManager.wrapperDTO.wrapper == null)
                throw new RomuloException($"Can't activate {mode} Transform mode because no object is selected");
            _transformSelectedManager.ActivateTransformMode(_selectedObjectsManager.wrapperDTO, mode);
        }

        private void DeactivateTransformMode()
        {
            if (!_transformSelectedManager.active)
                throw new RomuloException("Can't deactivate transform mode if no transform mode is currently active");
            _transformSelectedManager.DeactivateTransformMode();
        }


        private void ProccessEditMode(bool editMode)
        {
            _selectedObjectsManager.allowSelection = editMode;
            if (editMode == false)
            {
                _selectedObjectsManager.ClearSelection();
                if (_transformSelectedManager.active)
                    _transformSelectedManager.DeactivateTransformMode();
            }
            WebMessage<bool> message = new WebMessage<bool>
            {
                type = WebMessageType.setEditModeSuccess,
                payload = editMode,
            };
            _webMessageSender.SendWebMessage(message);
        }
        private void ProcessNewWrapper(ObjectWrapperDTO wrappedObject)
        {
            if (_transformSelectedManager.active)
            {
                _transformSelectedManager.wrapper = wrappedObject;
            }
            SendNewSelectedObjectsMessage(wrappedObject.wrappedObjects);
        }
        private void SendNewSelectedObjectsMessage(List<GameObject> selectedObjects)
        {
            List<ObjectDTO> selectedDTOObjects = new List<ObjectDTO>();
            foreach (GameObject obj in selectedObjects)
            {
                string objId = obj.GetComponent<IUniqueIdentifer>().getId();
                selectedDTOObjects.Add(new ObjectDTO
                {
                     id = objId,
                     tags = new string[1] {obj.tag},
                });
            }
            ObjectDTO[] objectDTOs = selectedDTOObjects.ToArray();
            WebMessage<ObjectDTO[]> message = new WebMessage<ObjectDTO[]>
            {
                type = WebMessageType.setSelectedObjects,
                payload = objectDTOs
            };
            _webMessageSender.SendWebMessage(message);
        }
        private void ProcessTransformModeActivation(TransformMode mode)
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
