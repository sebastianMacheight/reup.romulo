using ReupVirtualTwin.helpers;
using UnityEngine;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.behaviours
{
    public class SelectSceneTrigger : SelectObject
    {
        private IMaterialsContainerCreator _containerCreator;
        private MaterialsManager _materialsManager;
        protected override void Start()
        {
            base.Start();
            _containerCreator = GetComponent<IMaterialsContainerCreator>();
            _materialsManager = ObjectFinder.FindMaterialsManager().GetComponent<MaterialsManager>();
        }
        public override void HandleObject(GameObject triggerObject)
        {
            var materialSelectionTrigger = triggerObject.GetComponent<MaterialSelectionTrigger>();
            _materialsManager.SelectObjects(materialSelectionTrigger.materialObjects, materialSelectionTrigger.objectsMaterialIndexes);
            _containerCreator.CreateContainer(materialSelectionTrigger.selectableMaterials.ToArray());
        }
    }
}