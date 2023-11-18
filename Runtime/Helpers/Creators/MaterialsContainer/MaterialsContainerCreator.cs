using ReupVirtualTwin.inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReupVirtualTwin.helpers
{
    public abstract class MaterialsContainerCreator : MonoBehaviour, IMaterialsContainerCreator
    {
        public GameObject materialsContainerInstance { get; set; } = null;
        public abstract GameObject CreateContainer(Material[] selectableMaterials);
        public abstract void HideContainer();

        private bool _prevMaterialsContainerInstance;
        private InputProvider _inputProvider;

        protected void OnSelect(InputAction.CallbackContext obj)
        {
        }
    }
}