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

        IWebMessagesSender _webMessagesSender;
        public IWebMessagesSender webMessageSender
        {
            set { _webMessagesSender = value; }
        }


    }
}
