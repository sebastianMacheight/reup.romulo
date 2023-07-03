using UnityEngine;

namespace ReUpVirtualTwin
{
    public class ClickTrigger : SelectObject
    { 
        public override void HandleObject(GameObject triggerObject)
        {
            var extension = triggerObject.GetComponent<TriggerInstance>().extension;
            Debug.Log("you clicked the trigger " + triggerObject.name);
            extension.ActivateExtension();
        }
    }
}
