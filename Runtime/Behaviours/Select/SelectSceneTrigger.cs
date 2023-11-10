using UnityEngine;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.behaviours
{
    public class SelectSceneTrigger : SelectObject
    {
        public override void HandleObject(GameObject triggerObject)
        {
            var trigger = triggerObject.GetComponent<IMaterialSelectionTrigger>();
            trigger.CreateContainer();
        }
    }
}
