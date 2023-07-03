using ReUpVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ReUpVirtualTwin.Models
{
    public class MaterialSelectorExtension : Extension
    {
        public GameObject objectToReplaceMaterial;
        public Material[] selectableMaterials;

        public override string extensionName { get; set; }
        public override Trigger trigger { get; set; }
        public override GameObject extensionPanelItem { get; set; }

        public override void ActivateExtension()
        {
            var materialsManager = ObjectFinder.FindMaterialsManager();
            materialsManager.ShowMaterialsContainer(objectToReplaceMaterial, selectableMaterials);
        }

    }
}
