using UnityEngine;
using UnityEngine.InputSystem;
using ReupVirtualTwin.helpers;

public class DragManager : MonoBehaviour
{
    [HideInInspector]
    public bool dragging = false;
    [HideInInspector]
    public bool prevDragging = false;
    [HideInInspector]
    public bool selectInputInUI = false;
    [HideInInspector]
    public bool prevSelectInputInUI = false;

    private bool _selectInput = false;
    private Vector2 _selectPosition;
    private InputProvider _inputProvider;
    private float _dragDistanceThreshold = 2.0f;

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
        prevDragging = dragging;
        prevSelectInputInUI = selectInputInUI;
        if (_selectInput && dragging == false)
        {
            //dragging = Vector2.Distance(_inputProvider.PointerInput(), _selectPosition) > dragDitanceThreshold;
            if ( Vector2.Distance(_inputProvider.PointerInput(), _selectPosition) > _dragDistanceThreshold)
            {
                dragging = true;
                Debug.Log("sii");
            }
            else
            {
                dragging = false;
            }
        }
    }

    private void OnPress(InputAction.CallbackContext obj)
    {
        if (OverUICheck.PointerOverUI())
        {
            selectInputInUI = true;
        }
        else
        {
            _selectInput = true;
            _selectPosition = _inputProvider.PointerInput();
        }
    }

    private void OnPressCanceled(InputAction.CallbackContext obj)
    {
        _selectInput = false;
        dragging = false;
        selectInputInUI = false;
    }
}
