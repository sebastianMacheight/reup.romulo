using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManager : MonoBehaviour, IEditModeManager, IMediator, IEditModeWebManager
    {
        private bool _editMode = false;
        public bool editMode {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
                WebMessage message = new WebMessage
                {
                    operation = WebOperationsEnum.setEditMode,
                    body = _editMode ? "true" : "false"
                };
                _webMessagesSender.SendWebMessage(message);
            }
        }
        private ICharacterRotationManager _characterRotationManager;
        public ICharacterRotationManager characterRotationManager
        {
            set { _characterRotationManager = value; }
        }

        IWebMessagesSender _webMessagesSender;
        public IWebMessagesSender webMessageSender
        {
            set { _webMessagesSender = value; }
        }

        public void Notify(string eventName)
        {
            switch (eventName)
            {
                case EventsEnum.transformHandleStartIteraction:
                    _characterRotationManager.allowRotation = false;
                    break;
                case EventsEnum.transformHandleStopIteraction:
                    _characterRotationManager.allowRotation = true;
                    break;
                default:
                    throw new System.Exception($"no implementation for event: {eventName}");
            }
        }

        public void Notify(string eventName, string payload)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveSetEditModeRequest(string mode)
        {
            editMode = (mode == "true") ? true : false;
        }
    }
}
