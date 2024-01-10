using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;
using UnityEngine.XR;

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
                string payload = _editMode ? "true" : "false";
                WebMessage<string> message = new WebMessage<string>
                {
                    type = WebOperationsEnum.setEditModeSuccess,
                    payload = payload
                };
                _webMessagesSender.SendWebMessage(message);
                _mediator.Notify(Events.setEditMode, payload);
            }
        }

        private IMediator _mediator;
        public IMediator mediator { set { _mediator = value; } }

        IWebMessagesSender _webMessagesSender;
        public IWebMessagesSender webMessageSender { set { _webMessagesSender = value; } }


    }
}
