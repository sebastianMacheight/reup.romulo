using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.behaviours
{
    public class HideWebMaterials : SelectObject
    {
        IMaterialsContainerHider _webMaterialContainerHider;

        protected override void Start()
        {
            base.Start();
            _webMaterialContainerHider = ObjectFinder.FindextensionsTriggers().GetComponent<IMaterialsContainerHider>();
        }

        public override void MissObject()
        {
            SendHideWebContainerMessage();
            HideContainer();
        }
        void SendHideWebContainerMessage()
        {

            var message = new WebMessage
            {
                operation = "hideMaterialsOptions"
            };
            WebMessagesSender.SendWebMessage(message);
        }
        void HideContainer()
        {
            _webMaterialContainerHider.HideContainer();
        }
    }
}
