using ReupVirtualTwin.managerInterfaces;
using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managers
{
    public class EditionMediator : MonoBehaviour, IMediator, IEditModeWebReceiber
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
        public void Notify(string eventName, string payload)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveSetEditModeRequest(string mode)
        {
            _editModeManager.editMode = (mode == "true") ? true : false;
        }
    }
}
