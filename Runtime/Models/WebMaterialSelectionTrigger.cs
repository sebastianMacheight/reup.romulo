using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.models
{
    public class WebMaterialSelectionTrigger : MonoBehaviour, IMaterialSelectionTrigger
    {
        IWebMaterialContainerHandler _containerHandler;

        private void Start()
        {
            _containerHandler = ObjectFinder.FindextensionsTriggers().GetComponent<IWebMaterialContainerHandler>();
        }

        public GameObject CreateContainer()
        {
            return _containerHandler.CreateContainer(this);
        }
    }
}
