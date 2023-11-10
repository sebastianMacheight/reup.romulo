using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.models
{
    public class MaterialSelectionTrigger : MonoBehaviour, IMaterialSelectionTrigger
    {
        public List<Material> selectableMaterials;
        public List<GameObject> objects;
        [HideInInspector]
        public int[] objectsMaterialIndexes;


        IMaterialContainerHandler _containerHandler;

        private void Start()
        {
            _containerHandler = ObjectFinder.FindMaterialsContainerHandler().GetComponent<IMaterialContainerHandler>();
        }

        public GameObject CreateContainer()
        {
            return _containerHandler.CreateContainer(this);
        }
    }
}
