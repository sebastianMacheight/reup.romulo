using ReUpVirtualTwin.Helpers;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class ClickSceneTrigger : SelectObject
    { 
        public override void HandleObject(GameObject triggerObject)
        {
            var materialSelectionTrigger = triggerObject.GetComponent<MaterialSelectionTrigger>();
            var materialsManager = ObjectFinder.FindMaterialsManager();
            materialsManager.ShowMaterialsContainer(
                materialSelectionTrigger.materialObject,
                materialSelectionTrigger.materialIndex,
                materialSelectionTrigger.selectableMaterials.ToArray());
        }
    }
}
