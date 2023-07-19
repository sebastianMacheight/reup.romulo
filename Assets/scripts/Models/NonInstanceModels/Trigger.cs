using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ReUpVirtualTwin.Models
{
    public class Trigger
    {
        public GameObject triggerObject;
        public Vector3 triggerPosition
        {
            get
            { 
                return triggerObject.transform.position;
            }
            set 
            {
                triggerObject.transform.position = value;
            }
        }

        public Trigger(GameObject triggerObject, Vector3 triggerPosition
            //, GameObject objectToReplaceMaterial, List<MaterialItem> selectableMaterials
            )
        {
            this.triggerObject = triggerObject;
            this.triggerPosition = triggerPosition;
            //this.objectToReplaceMaterial = objectToReplaceMaterial;
            //this.selectableMaterials = selectableMaterials;
        }
    }
}
