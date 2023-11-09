using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class SelectMaterialSelection : SelectObject
    {
        IMaterialContainerHandler _materialContainerHandler;
        //MaterialsHelper _materialsManager;

        protected override void Start()
        {
            base.Start();
            _materialContainerHandler = ObjectFinder.FindMaterialsContainerHandler().GetComponent<IMaterialContainerHandler>();
            //_materialsManager = ObjectFinder.FindMaterialsManager().GetComponent<MaterialsHelper>();
        }
        public override void HandleObject(GameObject materialSelectionObject)
        {
            var material = materialSelectionObject.GetComponent<Renderer>().material;
            _materialContainerHandler.SetNewMaterial(material);
            //_materialsManager.SetNewMaterial(material);
        }
        public override void MissObject()
        {
            if (!_dragManager.dragging && !_dragManager.prevDragging)
            {
                _materialContainerHandler.HideContainer();
            }
        }
    }
}
