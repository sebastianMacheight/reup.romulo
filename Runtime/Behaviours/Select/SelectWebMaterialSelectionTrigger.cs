using UnityEngine;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.behaviours
{
    public class SelectWebMaterialSelectionTrigger : SelectObject
    {
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
            WebMessagesSender.SendWebMessage(createWebMaterialsContainerMessage);
        }
    }
}
