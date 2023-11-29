using UnityEngine;
using ReupVirtualTwin.characterMovement;

public class CharacterMovementKeyboard : MonoBehaviour
{
    [SerializeField]
    private Transform _innerCharacterTransform;

    private InputProvider _inputProvider;
    [SerializeField]
    private CharacterPositionManager _characterPositionManager;
    [SerializeField]
    private CharacterRotationManager _characterRotationManager;
    private float _walkSpeed = 3.5f;


    private void Awake()
    {
        _inputProvider = new InputProvider();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 inputValue = _inputProvider.MovementInput().normalized;
        PerformForwardBackwardMovement(inputValue.y);
        PerformRotation(inputValue.x);
    }
    private void PerformRotation(float direction)
    {
        if (direction != 0f)
        {
            Debug.Log($"the direction is {direction}");
            _characterRotationManager.horizontalRotation += direction;
        }
    }

    private void PerformForwardBackwardMovement(float direction)
    {
        Vector3 movementDirection =  direction * GetCharacterForward();
        if (movementDirection != Vector3.zero && _characterPositionManager.allowWalking)
        {
            _characterPositionManager.StopWalking();
            _characterPositionManager.MoveInDirection(movementDirection, _walkSpeed);
        }
    }

    private Vector3 GetCharacterForward()
    {
        Vector3 forward = _innerCharacterTransform.forward;
        forward.y = 0;
        return forward;
    }
}
