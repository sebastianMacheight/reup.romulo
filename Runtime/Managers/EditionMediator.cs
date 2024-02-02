using ReupVirtualTwin.managerInterfaces;
using UnityEngine;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using System;
using System.Collections.Generic;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.helperInterfaces;

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
            set { _editModeManager = value; }
        }
        private ISelectedObjectsManager _selectedObjectsManager;
        public ISelectedObjectsManager selectedObjectsManager { set { _selectedObjectsManager = value; } }
        private ITransformObjectsManager _transformObjectsManager;
        public ITransformObjectsManager transformObjectsManager { set => _transformObjectsManager = value; }
        private IDeleteObjectsManager _deleteObjectsManager;
        public IDeleteObjectsManager deleteObjectsManager { set => _deleteObjectsManager = value; }

        private IInsertObjectsManager _insertObjectsManager;
        public IInsertObjectsManager insertObjectsManager { set => _insertObjectsManager = value; }

        private IWebMessagesSender _webMessageSender;
        public IWebMessagesSender webMessageSender { set { _webMessageSender = value; } }
        private IObjectMapper _objectMapper;
        public IObjectMapper objectMapper { set => _objectMapper = value; }


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
                case Events.objectsDeleted:
                    ProcessDeleteObjects();
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
                case Events.insertedObjectLoaded:
                    if (!(payload is GameObject))
                    {
                        throw new ArgumentException($"Payload must be of type {nameof(GameObject)} for {eventName} events", nameof(payload));
                    }
                    ProcessInsertedObjectLoaded((GameObject)(object)payload);
                    break;
                case Events.insertedObjectStatusUpdate:
                    if (!(payload is float))
                    {
                        throw new ArgumentException($"Payload must be of type float for {eventName} events", nameof(payload));
                    }
                    ProcessLoadStatus((float)(object)payload);
                    break;
                case Events.restoreSelection:
                    if (!(payload is List<GameObject>))
                    {
                        throw new ArgumentException($"Payload must be of type List<GameObject> for {eventName} events", nameof(payload));
                    }
                    RestoreSelection(payload as List<GameObject>);
                    break;
                default:
                    throw new ArgumentException($"no implementation for event: {eventName}");
            }
        }


        public void ReceiveWebMessage(string serializedWebMessage)
        {
            WebMessage<string> message = JsonUtility.FromJson<WebMessage<string>>(serializedWebMessage);
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
                case WebMessageType.deleteObjects:
                    DeleteSelectedObjects();
                    break;
                case WebMessageType.loadObject:
                    LoadObject(message.payload);
                    break;
                default:
                    _webMessageSender.SendWebMessage(new WebMessage<string>
                    {
                        type = WebMessageType.error,
                        payload = $"message type:'{message.type}' not supported",
                    });
                    break;
            }
        }

        private void ActivateTransformMode(TransformMode mode)
        {
            if (_selectedObjectsManager.wrapperDTO == null || _selectedObjectsManager.wrapperDTO.wrapper == null)
            {
                SendErrorMessage($"Can't activate {mode} Transform mode because no object is selected");
                return;
            }
            _transformObjectsManager.ActivateTransformMode(_selectedObjectsManager.wrapperDTO, mode);
        }

        private void DeactivateTransformMode()
        {
            if (!_transformObjectsManager.active)
            {
                SendErrorMessage("Can't deactivate transform mode if no transform mode is currently active");
                return;
            }
            _transformObjectsManager.DeactivateTransformMode();
        }

        private void DeleteSelectedObjects()
        {
            if (_selectedObjectsManager.wrapperDTO == null || _selectedObjectsManager.wrapperDTO.wrapper == null)
                SendErrorMessage($"Unable to activate delete because no object is selected");
            _deleteObjectsManager.DeleteSelectedObjects(_selectedObjectsManager.wrapperDTO);
            //We clear the selected objects
            _selectedObjectsManager.ClearSelection();
        }

        private void ProccessEditMode(bool editMode)
        {
            _selectedObjectsManager.allowSelection = editMode;
            if (editMode == false)
            {
                _selectedObjectsManager.ClearSelection();
                if (_transformObjectsManager.active)
                    _transformObjectsManager.DeactivateTransformMode();
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
            if (_transformObjectsManager.active)
            {
                _transformObjectsManager.wrapper = wrappedObject;
            }
            SendNewSelectedObjectsMessage(wrappedObject.wrappedObjects);
        }

        private void SendNewSelectedObjectsMessage(List<GameObject> selectedObjects)
        {
            ObjectDTO[] objectDTOs = _objectMapper.MapObjectsToDTO(selectedObjects);
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

        private void ProcessDeleteObjects()
        {
            string webMessageType;
            webMessageType = WebMessageType.deleteObjectsSuccess;

            WebMessage<string> message = new WebMessage<string>
            {
                type = webMessageType,
            };
            _webMessageSender.SendWebMessage(message);
        }

        private void ProcessInsertedObjectLoaded(GameObject obj)
        {
            ObjectDTO objectDTO = _objectMapper.MapObjectToDTO(obj);
            WebMessage<ObjectDTO> message = new WebMessage<ObjectDTO>
            {
                type = WebMessageType.loadObjectSuccess,
                payload = objectDTO
            };
            _webMessageSender.SendWebMessage(message);
        }

        private void LoadObject(string url)
        {
            _insertObjectsManager.InsertObjectFromUrl(url);
        }

        private void ProcessLoadStatus(float status)
        {
            WebMessage<float> message = new WebMessage<float>
            {
                type = WebMessageType.loadObjectProcessUpdate,
                payload = status
            };
            _webMessageSender.SendWebMessage(message);
        }

        private void RestoreSelection(List<GameObject> objectsToRestore)
        {
            foreach (var gameObject in objectsToRestore)
            {
                _selectedObjectsManager.AddObjectToSelection(gameObject);
            }
        }

        private void SendErrorMessage(string message)
        {
            _webMessageSender.SendWebMessage(new WebMessage<string>
            {
                type = WebMessageType.error,
                payload = message,
            });
        }
    }

}

