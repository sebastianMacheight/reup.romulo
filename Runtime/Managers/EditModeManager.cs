using ReupVirtualTwin.enums;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManager : MonoBehaviour, IEditModeManager, IMediator
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

        public void Notify(string eventName, string payload)
        {
            throw new System.NotImplementedException();
        }
    }
}
