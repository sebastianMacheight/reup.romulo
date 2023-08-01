using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class MaterialsContainerCreator : MonoBehaviour, IMaterialsContainerCreator
{
    public GameObject materialsContainerInstance { get; set; } = null;
    public abstract void CreateContainer(GameObject obj, Material[] selectableMaterials);
    public abstract void HideContainer();

    private bool _prevMaterialsContainerInstance;
    private InputProvider _inputProvider;

    protected void OnSelect(InputAction.CallbackContext obj)
    {
    }
}
