using ReUpVirtualTwin.Helpers;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class ClickMaterialSelection : SelectObject
    { 
        IMaterialsContainerCreator materialCreator;
        MaterialsManager materialsManager;
        DragManager dragManager;

        private void Start()
        {
            materialCreator = ObjectFinder.FindMaterialsManager().gameObject.GetComponent<IMaterialsContainerCreator>();
            materialsManager = ObjectFinder.FindMaterialsManager();
            dragManager = ObjectFinder.FindDragManager();
        }
        public override void HandleObject(GameObject materialSelectionObject)
        {
            //Debug.Log("you clicked the material Selection object " + materialSelectionObject);
            var material = materialSelectionObject.GetComponent<Renderer>().material;
            materialsManager.SetNewMaterial(material);
        }
        public override void MissObject()
        {
            if (!dragManager.dragging && !dragManager.prevDragging)
            {
                //Debug.Log("Miss material selection, so hiding materials");
                materialCreator.HideContainer();
            }
        }
    }
}
