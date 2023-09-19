using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin
{
    public class ClickMaterialSelection : SelectObject
    {
        IMaterialsContainerCreator _materialContainerCreator;
        MaterialsManager _materialsManager;

        private void Start()
        {
            _materialContainerCreator = ObjectFinder.FindMaterialsContainerCreator();
            _materialsManager = ObjectFinder.FindMaterialsManager().GetComponent<MaterialsManager>();
        }
        public override void HandleObject(GameObject materialSelectionObject)
        {
            //Debug.Log("you clicked the material Selection object " + materialSelectionObject);
            var material = materialSelectionObject.GetComponent<Renderer>().material;
            _materialsManager.SetNewMaterial(material);
        }
        public override void MissObject()
        {
            if (!_dragManager.dragging && !_dragManager.prevDragging)
            {
                //Debug.Log("Miss material selection, so hiding materials");
                _materialContainerCreator.HideContainer();
            }
        }
    }
}
