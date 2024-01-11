using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManager : MonoBehaviour, IEditModeManager
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
                    type = WebOperationsEnum.setEditModeSuccess,
                    payload = _editMode ? "true" : "false"
                };
                _webMessagesSender.SendWebMessage(message);
            }
        }
        private ICharacterRotationManager _characterRotationManager;
        public ICharacterRotationManager characterRotationManager
        {
            set { _characterRotationManager = value; }
        }

        public void Notify(string eventName)
        {
            switch (eventName)
            {
                case EventsEnum.transformHandleStartInteraction:
                    _characterRotationManager.allowRotation = false;
                    break;
                case EventsEnum.transformHandleStopInteraction:
                    _characterRotationManager.allowRotation = true;
                    break;
                default:
                    throw new System.Exception($"no implementation for event: {eventName}");
            }
        }

        IWebMessagesSender _webMessagesSender;
        public IWebMessagesSender webMessageSender
        {
            set { _webMessagesSender = value; }
        }


    }
}
