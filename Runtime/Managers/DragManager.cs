using UnityEngine;
using UnityEngine.InputSystem;
using ReupVirtualTwin.helpers;

public class DragManager : MonoBehaviour
{
    public bool dragging = false;
    public bool prevDragging = false;
    public float dragDitanceThreshold = 10f;
    public bool selectInputInUI = false;
    public bool prevSelectInputInUI = false;

    private bool _isHolding = false;
    private Vector2 _selectPosition;
    private InputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = new InputProvider();
    }

    private void OnEnable()
    {
        _inputProvider.holdStarted += OnHold;
        _inputProvider.holdCanceled += OnHoldCanceled;
    }

    private void OnDisable()
    {
        _inputProvider.holdStarted -= OnHold;
        _inputProvider.holdCanceled -= OnHoldCanceled;
    }

    void Update()
    {
        //todo:
        //this is weird, check if there is a way to implement this withouth the need to check in every frame
        //may this helps
        //https://answers.unity.com/questions/1879168/check-if-ui-was-clicked-with-unity-new-input-syste.html

        //// Check for mouse input
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Check if the cursor is over a UI element
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        // Cursor is over a UI element
        //        Debug.Log("Cursor is over a UI element!");
        //    }
        //    else
        //    {
        //        // Cursor is NOT over a UI element
        //        Debug.Log("Cursor is NOT over a UI element!");
        //    }
        //}

        //// Check for touch input
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    // Check if the touch is over a UI element
        //    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //    {
        //        // Touch is over a UI element
        //        Debug.Log("Touch is over a UI element!");
        //    }
        //    else
        //    {
        //        // Touch is NOT over a UI element
        //        Debug.Log("Touch is NOT over a UI element!");
        //    }
        //}

        //pointerUnderUi = EventSystem.current != null ? EventSystem.current.IsPointerOverGameObject() : false;
        //Debug.Log($"the pointer Under UI is {pointerUnderUi}");

        prevDragging = dragging;
        prevSelectInputInUI = selectInputInUI;
        if (_isHolding == true && dragging == false)
        {
            var pointer = _inputProvider.PointerInput();
            var distance = Vector2.Distance(pointer, _selectPosition);
            dragging = distance > dragDitanceThreshold;
        }
    }

    private void OnHold(InputAction.CallbackContext obj)
    {
        if (OverUICheck.PointerOverUI())
        {
            selectInputInUI = true;
        }
        else
        {
            _isHolding = true;
            _selectPosition = _inputProvider.PointerInput();
        }
    }

    private void OnHoldCanceled(InputAction.CallbackContext obj)
    {
        _isHolding = false;
        dragging = false;
        selectInputInUI = false;
    }
}
