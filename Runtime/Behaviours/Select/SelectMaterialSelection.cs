using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class SelectMaterialSelection : SelectObject
    {
        IMaterialsContainerCreator _materialContainerCreator;
        MaterialsManager _materialsManager;

        private void Start()
        {
            _materialContainerCreator = ObjectFinder.FindMaterialsContainerCreator().GetComponent<IMaterialsContainerCreator>();
            _materialsManager = ObjectFinder.FindMaterialsManager().GetComponent<MaterialsManager>();
        }
        public override void HandleObject(GameObject materialSelectionObject)
        {
            var material = materialSelectionObject.GetComponent<Renderer>().material;
            _materialsManager.SetNewMaterial(material);
        }
        public override void MissObject()
        {
            if (!_dragManager.dragging && !_dragManager.prevDragging)
            {
                _materialContainerCreator.HideContainer();
            }
        }
    }
}
