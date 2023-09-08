using ReUpVirtualTwin.Helpers;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class ClickSceneTrigger : SelectObject
    { 
        private IMaterialsContainerCreator _containerCreator;
        private MaterialsManager _materialsManager;
        protected override void Awake()
        {
            base.Awake();
            _containerCreator = GetComponent<IMaterialsContainerCreator>();
            _materialsManager = ObjectFinder.FindMaterialsManager();
        }
        public override void HandleObject(GameObject triggerObject)
        {
            var materialSelectionTrigger = triggerObject.GetComponent<MaterialSelectionTrigger>();
            _materialsManager.SelectObjects( materialSelectionTrigger.materialObjects, materialSelectionTrigger.materialIndexes);
            _containerCreator.CreateContainer(materialSelectionTrigger.selectableMaterials.ToArray());
        }
    }
}
