using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.behaviours
{
    public class HideWebMaterials : SelectObject
    {
        IMaterialsContainerHider _webMaterialContainerHandler;

        protected override void Start()
        {
            base.Start();
            _webMaterialContainerHandler = ObjectFinder.FindextensionsTriggers().GetComponent<IMaterialsContainerHider>();
        }

        public override void MissObject()
        {
            _webMaterialContainerHandler.HideContainer();
        }

    }
}
