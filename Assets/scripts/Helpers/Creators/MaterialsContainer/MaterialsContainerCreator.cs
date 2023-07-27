using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class MaterialsContainerCreator : MonoBehaviour, IMaterialsContainerCreator
{
    //public DragManager dragManager;
    public GameObject materialsContainerInstance { get; set; } = null;
    public abstract void CreateContainer(Material[] selectableMaterials);
    public abstract void HideContainer();

    private bool _prevMaterialsContainerInstance;
    private InputProvider _inputProvider;

    //private void Awake()
    //{
    //    _inputProvider = new InputProvider();
    //}

    //private void OnEnable()
    //{
    //    _inputProvider.selectCanceled += OnSelect;
    //}

    //private void OnDisable()
    //{
    //    _inputProvider.selectCanceled -= OnSelect;
    //}

    void Update()
    {

        // Update if we have a materials Container Instance
  //      if (materialsContainerInstance != null) {
  //          _prevMaterialsContainerInstance = true;
		//}
  //      else {
  //          _prevMaterialsContainerInstance = false;
		//}
    }

    protected void OnSelect(InputAction.CallbackContext obj)
    {
        //Hide materials container if mouse clicks somewhere else
        //if (materialsContainerInstance != null  && !dragManager.prevDragging && _prevMaterialsContainerInstance)
        //{
            //Debug.Log("hiding the material container");
            //HideContainer();
		//}
    }
}
