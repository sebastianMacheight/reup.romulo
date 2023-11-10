using UnityEngine;

namespace ReupVirtualTwin.models
{
    public interface IWebMaterialContainerHandler : IMaterialsContainerHider
    {
        public GameObject CreateContainer(WebMaterialSelectionTrigger trigger);
        public void SetNewMaterial();
    }
}