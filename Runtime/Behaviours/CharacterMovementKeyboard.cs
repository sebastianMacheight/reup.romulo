using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ReupVirtualTwin.characterMovement;

[RequireComponent(typeof(CharacterPositionManager))]
public class CharacterMovementKeyboard : MonoBehaviour
{

    private InputProvider _inputProvider;
    private CharacterPositionManager _characterPositionManager;
    [SerializeField]
    private Transform _characterBodyTransform;


    private void Awake()
    {
        _inputProvider = new InputProvider();
        _characterPositionManager = GetComponent<CharacterPositionManager>();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 inputValue = _inputProvider.MovementInput().normalized;
        Vector3 movementDirection = inputValue.x * GetCharacterRight() +
                            inputValue.y * GetCharacterForward();
        if (movementDirection != Vector3.zero)
        {
            _characterPositionManager.MovePositionByStepInDirection(movementDirection);
        }
    }

    private Vector3 GetCharacterRight()
    {
        Vector3 right = _characterBodyTransform.right;
        right.y = 0;
        return right;
    }
    private Vector3 GetCharacterForward()
    {
        Vector3 forward = _characterBodyTransform.forward;
        forward.y = 0;
        return forward;
    }
}
