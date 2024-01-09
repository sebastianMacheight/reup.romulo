using ReupVirtualTwin.managerInterfaces;
using UnityEngine;
using ReupVirtualTwin.enums;
using System.Collections;

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
        private ISelectedObjectsManager _selectedObjectsManager;
        public ISelectedObjectsManager selectedObjectsManager { set { _selectedObjectsManager = value; } }

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
                    throw new System.Exception($"no implementation for event: {eventName}");
            }
        }
        public void Notify(Events eventName, string payload)
        {
            switch(eventName)
            {
                case Events.setEditMode:
                    if (payload == "false")
                    {
                        _selectedObjectsManager.ClearSelection();
                    }
                    break;
                default:
                    throw new System.Exception($"no implementation with payload for event: {eventName}");
            }
        }

        public void ReceiveSetEditModeRequest(string mode)
        {
            _editModeManager.editMode = (mode == "true") ? true : false;
        }
    }
}
