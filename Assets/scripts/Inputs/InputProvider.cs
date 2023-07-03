using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider
{
    private static AppInputActions _input = new ();

    public event Action<InputAction.CallbackContext> selectStarted
    {
        add
	    {
            _input.Player.Select.started += value;
		}
        remove
	    {
            _input.Player.Select.started -= value;
		}
    }
    public event Action<InputAction.CallbackContext> selectPerformed
    {
        add
	    {
            _input.Player.Select.performed += value;
		}
        remove
	    {
            _input.Player.Select.performed -= value;
		}
    }
    public event Action<InputAction.CallbackContext> selectCanceled
    {
        add
	    {
            _input.Player.Select.canceled += value;
		}
        remove
	    {
            _input.Player.Select.canceled -= value;
		}
    }

    public void Enable()
    {
        _input.Player.RotateView.Enable();
        _input.Player.RotateViewKeyboard.Enable();
        _input.Player.Movement.Enable();
        _input.Player.Select.Enable();
        _input.Player.Pointer.Enable();
    }

    public void Disable()
    {
        _input.Player.RotateView.Disable();
        _input.Player.RotateViewKeyboard.Disable();
        _input.Player.Movement.Disable();
        _input.Player.Select.Disable();
        _input.Player.Pointer.Disable();
    }

    public Vector2 RotateViewInput()
    {
        return _input.Player.RotateView.ReadValue<Vector2>() * -1;
    }

    public Vector2 RotateViewKeyboardInput()
    { 
        return _input.Player.RotateViewKeyboard.ReadValue<Vector2>();
    }

    public Vector2 MovementInput()
    { 
        return _input.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 PointerInput()
    { 
        return _input.Player.Pointer.ReadValue<Vector2>();
    }
}
