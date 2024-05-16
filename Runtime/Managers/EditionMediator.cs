using UnityEngine;
using System;
using System.Collections.Generic;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.helpers;
using Newtonsoft.Json;


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
        public IEditModeManager editModeManager { set => _editModeManager = value; }
        private ISelectedObjectsManager _selectedObjectsManager;
        public ISelectedObjectsManager selectedObjectsManager { set { _selectedObjectsManager = value; } }
        private ITransformObjectsManager _transformObjectsManager;
        public ITransformObjectsManager transformObjectsManager { set => _transformObjectsManager = value; }
        private IDeleteObjectsManager _deleteObjectsManager;
        public IDeleteObjectsManager deleteObjectsManager { set => _deleteObjectsManager = value; }
 
        private IChangeColorManager _changeColorManager;
        public IChangeColorManager changeColorManager { set => _changeColorManager = value; }

        private IInsertObjectsController _insertObjectsManager;
        public IInsertObjectsController insertObjectsController { set => _insertObjectsManager = value; }

        private IWebMessagesSender _webMessageSender;
        public IWebMessagesSender webMessageSender { set { _webMessageSender = value; } }
        private IObjectMapper _objectMapper;
        public IObjectMapper objectMapper { set => _objectMapper = value; }

        private IObjectRegistry _registry;
        public IObjectRegistry registry { set => _registry = value; get => _registry; }

        private IChangeMaterialController _changeMaterialController;
        public IChangeMaterialController changeMaterialController
        {
            get => _changeMaterialController; set => _changeMaterialController = value;
        }

        [HideInInspector]
        public string noInsertObjectIdErrorMessage = "No object id provided for insertion";
        [HideInInspector]
        public string noInsertObjectUrlErrorMessage = "No 3d model url provided for insertion";

        public string InvalidColorErrorMessage(string colorCode) => $"Invalid color code {colorCode}";

        private IModelInfoManager _modelInfoManager;
        public IModelInfoManager modelInfoManager { set => _modelInfoManager = value; }

        public void Notify(ReupEvent eventName)
        {
            switch (eventName)
            {
                case ReupEvent.transformHandleStartItneraction:
                    _characterRotationManager.allowRotation = false;
                    break;
                case ReupEvent.transformHandleStopInteraction:
                    _characterRotationManager.allowRotation = true;
                    break;
                case ReupEvent.positionTransformModeActivated:
                    ProcessTransformModeActivation(TransformMode.PositionMode);
                    break;
                case ReupEvent.rotationTransformModeActivated:
                    ProcessTransformModeActivation(TransformMode.RotationMode);
                    break;
                case ReupEvent.transformModeDeactivated:
                    ProcessTranformModeDeactivation();
                    break;
                case ReupEvent.objectsDeleted:
                    ProcessDeletedObjects();
                    break;
                case ReupEvent.objectColorChanged:
                    ProcessObjectColorChanged();
                    break;
                default:
                    throw new ArgumentException($"no implementation without payload for event: {eventName}");
            }
        }
        public void Notify<T>(ReupEvent eventName, T payload)
        {
            switch (eventName)
            {
                case ReupEvent.setEditMode:
                    if (!(payload is bool))
                    {
                        throw new ArgumentException($"Payload must be a boolean for {eventName} events", nameof(payload));
                    }
                    ProccessEditMode((bool)(object)payload);
                    break;
                case ReupEvent.setSelectedObjects:
                    if (!(payload is ObjectWrapperDTO))
                    {
                        throw new ArgumentException($"Payload must be of type {nameof(ObjectWrapperDTO)} for {eventName} events", nameof(payload));
                    }
                    ProcessNewWrapper((ObjectWrapperDTO)(object)payload);
                    break;
                case ReupEvent.insertedObjectLoaded:
                    if (!(payload is InsertedObjectPayload))
                    {
                        throw new ArgumentException($"Payload must be of type {nameof(InsertedObjectPayload)} for {eventName} events", nameof(payload));
                    }
                    ProcessInsertedObjectLoaded((InsertedObjectPayload)(object)payload);
                    break;
                case ReupEvent.insertedObjectStatusUpdate:
                    if (!(payload is float))
                    {
                        throw new ArgumentException($"Payload must be of type float for {eventName} events", nameof(payload));
                    }
                    ProcessLoadStatus((float)(object)payload);
                    break;
                case ReupEvent.objectMaterialChanged:
                    if (!(payload is ChangeMaterialMessagePayload))
                    {
                        throw new ArgumentException($"Payload must be of type ChangeMaterialMessagePayload for {eventName} events", nameof(payload));
                    }
                    ProcessObjectMaterialsChange((ChangeMaterialMessagePayload)(object)payload);
                    break;
                case ReupEvent.error:
                    if (!(payload is string))
                    {
                        throw new ArgumentException($"Payload must be of type string for {eventName} events", nameof(payload));
                    }
                    SendErrorMessage((string)(object)payload);
                    break;
                default:
                    throw new ArgumentException($"no implementation for event: {eventName}");
            }
        }


        public void ReceiveWebMessage(string serializedWebMessage)
        {
            Dictionary<string, object> message = JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedWebMessage);
            string type = (string) DataManipulationHelpers.GetValueAtPath(message, new string[] { "type" });
            object payload = DataManipulationHelpers.GetValueAtPath(message, new string[] { "payload" });
            switch (type)
            {
                case WebMessageType.setEditMode:
                    _editModeManager.editMode = ((string)payload).ToLower() == "true";
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
                    DeleteSelectedObjects((string)payload);
                    break;
                case WebMessageType.loadObject:
                    LoadObject((string)payload);
                    break;
                case WebMessageType.changeObjectColor:
                    ChangeObjectsColor((string)payload);
                    break;
                case WebMessageType.requestModelInfo:
                    SendModelInfoMessage();
                    break;
                case WebMessageType.changeObjectsMaterial:
                    ChangeObjectsMaterial((Dictionary<string, object>)payload);
                    break;
                default:
                    _webMessageSender.SendWebMessage(new WebMessage<string>
                    {
                        type = WebMessageType.error,
                        payload = $"message type:'{type}' not supported",
                    });
                    break;
            }
        }

        public void SendModelInfoMessage()
        {
            WebMessage<ModelInfoMessage> message = _modelInfoManager.ObtainModelInfoMessage();
            _webMessageSender.SendWebMessage(message);
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

        private void DeleteSelectedObjects(string stringIds)
        {
            List<GameObject> objectsToDelete = _deleteObjectsManager.GetDeletableObjects(stringIds);
            if (objectsToDelete.Count > 0)
            {
                foreach( GameObject obj in objectsToDelete)
                {
                    _selectedObjectsManager.RemoveObjectFromSelection(obj);
                }
                _deleteObjectsManager.DeleteObjects(objectsToDelete);
            }
            else
            {
                SendErrorMessage("The selection is empty, or there is at least one non-deletable object selected");
            }

        }

        private void ChangeObjectsColor(string payload)
        {
            ChangeColorObjectMessagePayload parsedPayload = JsonUtility.FromJson<ChangeColorObjectMessagePayload>(payload);
            List<GameObject> objectsToChangeColor = _registry.GetObjectsWithGuids(parsedPayload.objectIds);
            if (objectsToChangeColor.Count > 0)
            {
                Color? parsedColor = Utils.ParseColor(parsedPayload.color);
                if (parsedColor != null)
                {
                    _changeColorManager.ChangeObjectsColor(objectsToChangeColor, (Color)parsedColor);
                }
                else
                {
                    SendErrorMessage(InvalidColorErrorMessage(parsedPayload.color));
                }
            }
            else
            {
                SendErrorMessage("The selection is empty");
            }
        }

        private void ChangeObjectsMaterial(Dictionary<string, object> payload)
        {
            var materialUrl = payload.GetValueAtPath(new string[] { "material_url" });
            var objectIds = payload.GetValueAtPath(new string[] { "object_ids" });
            Debug.Log("materialUrl");
            Debug.Log(materialUrl is string);
            Debug.Log(materialUrl);
            Debug.Log("objectIds");
            Debug.Log(objectIds is string[]);
            Debug.Log(objectIds is object[]);
            Debug.Log(((object[])objectIds).Length);
            ChangeMaterialMessagePayload a = new()
            {
                material_url = (string)materialUrl,
                object_ids = (string[])objectIds,
            };
            _changeMaterialController.ChangeObjectMaterial(a);
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
                throw new Exception($"unknown TransformMode {mode}");
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

        private void ProcessDeletedObjects()
        {
            string webMessageType;
            webMessageType = WebMessageType.deleteObjectsSuccess;

            WebMessage<string> message = new WebMessage<string>
            {
                type = webMessageType,
            };
            _webMessageSender.SendWebMessage(message);
        }

        private void ProcessObjectColorChanged()
        {
            WebMessage<string> message = new WebMessage<string>
            {
                type = WebMessageType.changeObjectColorSuccess,
            };
        }

        private void ProcessInsertedObjectLoaded(InsertedObjectPayload insertedObjectPayload)
        {
            SendInsertedObjectMessage(insertedObjectPayload.loadedObject);
            if (insertedObjectPayload.selectObjectAfterInsertion)
            {
                if (insertedObjectPayload.deselectPreviousSelection)
                {
                    _selectedObjectsManager.ClearSelection();
                }
                _selectedObjectsManager.AddObjectToSelection(insertedObjectPayload.loadedObject);
            }
        }
        private void SendInsertedObjectMessage(GameObject obj)
        {
            ObjectDTO objectDTO = _objectMapper.MapObjectToDTO(obj);
            WebMessage<ObjectDTO> message = new WebMessage<ObjectDTO>
            {
                type = WebMessageType.loadObjectSuccess,
                payload = objectDTO
            };
            _webMessageSender.SendWebMessage(message);
        }

        private void LoadObject(string payload)
        {
            InsertObjectMessagePayload parsedPayload = JsonUtility.FromJson<InsertObjectMessagePayload>(payload);
            if (parsedPayload.objectUrl == null || parsedPayload.objectUrl == "")
            {
                SendErrorMessage(noInsertObjectUrlErrorMessage);
                return;
            }
            if (parsedPayload.objectId == null || parsedPayload.objectId == "")
            {
                SendErrorMessage(noInsertObjectIdErrorMessage);
                return;
            }
            _insertObjectsManager.InsertObject(parsedPayload);
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

        private void ProcessObjectMaterialsChange(ChangeMaterialMessagePayload materialsChangedInfo)
        {
            WebMessage<ChangeMaterialMessagePayload> message = new()
            {
                type = WebMessageType.changeObjectsMaterialSuccess,
                payload = materialsChangedInfo
            };
            _webMessageSender.SendWebMessage(message);
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

