using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.models
{
    public class MaterialSelectionTrigger : MonoBehaviour
    {
        public List<GameObject> materialObjects;
        [HideInInspector]
        public int[] materialIndexes;
        public List<Material> selectableMaterials;
    }
}
