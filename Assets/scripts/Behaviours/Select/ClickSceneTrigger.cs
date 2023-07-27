using ReUpVirtualTwin.Helpers;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class ClickSceneTrigger : SelectObject
    { 
        public override void HandleObject(GameObject triggerObject)
        {
            //Debug.Log("you clicked the trigger " + triggerObject.name);
            var materialSelectionTrigger = triggerObject.GetComponent<MaterialSelectionTrigger>();
            var materialsManager = ObjectFinder.FindMaterialsManager();
            materialsManager.ShowMaterialsContainer(
                materialSelectionTrigger.materialObjects,
                materialSelectionTrigger.materialIndexes,
                materialSelectionTrigger.selectableMaterials.ToArray());
        }
    }
}
