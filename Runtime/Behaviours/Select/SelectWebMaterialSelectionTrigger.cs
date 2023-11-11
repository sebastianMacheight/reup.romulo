using UnityEngine;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.behaviours
{
    [RequireComponent(typeof(IWebMessagesSender))]
    public class SelectWebMaterialSelectionTrigger : SelectObject
    {
        IWebMessagesSender _webMessagesSender;

        protected override void Start()
        {
            base.Start();
            _webMessagesSender = GetComponent<IWebMessagesSender>();
        }

        public override void HandleObject(GameObject triggerObject)
        {
            var trigger = triggerObject.GetComponent<IWebMaterialSelectionTrigger>();
            var container = CreateContainer(trigger);
            if (container != null)
            {
                SendCreateWebContainerMessage(trigger);
            }
        }
        GameObject CreateContainer(IWebMaterialSelectionTrigger trigger)
        {
            return trigger.CreateContainer();
        }
        void SendCreateWebContainerMessage(IWebMaterialSelectionTrigger trigger)
        {
            var createWebMaterialsContainerMessage = trigger.GetWebContainerMessage();
            _webMessagesSender.SendWebMessage(createWebMaterialsContainerMessage);
        }
    }
}
