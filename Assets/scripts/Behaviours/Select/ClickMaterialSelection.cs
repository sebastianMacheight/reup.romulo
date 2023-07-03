using ReUpVirtualTwin.Helpers;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class ClickMaterialSelection : SelectObject
    { 
        public override void HandleObject(GameObject materialSelectionObject)
        {
            //Debug.Log("you clicked the material Selection object " + materialSelectionObject);
            var materialsManager = ObjectFinder.FindMaterialsManager();
            var material = materialSelectionObject.GetComponent<Renderer>().material;
            materialsManager.SetNewMaterial(material);
        }
        public override void MissObject()
        {
            //Debug.Log("miss the material selection");
        }
    }
}
