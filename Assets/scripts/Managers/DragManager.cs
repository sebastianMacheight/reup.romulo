using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    public bool dragging = false;
    public bool prevDragging = false;
    public bool pointerUnderUi = false;
    public float dragDitanceThreshold = 10f;

    private bool _selectInput = false;
    private Vector3 _selectPosition;
    private InputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = new InputProvider();
    }

    private void OnEnable()
    {
        _inputProvider.selectPerformed += OnPress;
        _inputProvider.selectCanceled += OnPressCanceled;
    }

    private void OnDisable()
    {
        _inputProvider.selectPerformed -= OnPress;
        _inputProvider.selectCanceled -= OnPressCanceled;
    }

    void Update()
    {
        //todo:
        //this is weird, check if there is a way to implement this withouth the need to check in every frame
        //may this helps
        //https://answers.unity.com/questions/1879168/check-if-ui-was-clicked-with-unity-new-input-syste.html

        pointerUnderUi = EventSystem.current != null ? EventSystem.current.IsPointerOverGameObject() : false;

        prevDragging = dragging;
        if (_selectInput && dragging == false)
        {
            dragging = Vector3.Distance(_inputProvider.PointerInput(), _selectPosition) > dragDitanceThreshold;
        }
    }

    private void OnPress(InputAction.CallbackContext obj)
    {
        if (!pointerUnderUi)
        {
            _selectInput = true;
            _selectPosition = _inputProvider.PointerInput();
        }
    }

    private void OnPressCanceled(InputAction.CallbackContext obj)
    {
        _selectInput = false;
        dragging = false;
    }
}
