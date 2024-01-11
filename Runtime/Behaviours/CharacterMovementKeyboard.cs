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
    private float WALK_SPEED_M_PER_SECOND = 3.5f;
    private float ROTATION_SPEED_DEG_PER_SECOND = 180f;


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
            float angleDelta = direction * ROTATION_SPEED_DEG_PER_SECOND * Time.deltaTime;
            _characterRotationManager.horizontalRotation += angleDelta;
        }
    }

    private void PerformForwardBackwardMovement(float direction)
    {
        Vector3 movementDirection =  direction * GetCharacterForward();
        if (movementDirection != Vector3.zero && _characterPositionManager.allowWalking)
        {
            _characterPositionManager.StopWalking();
            _characterPositionManager.MoveInDirection(movementDirection, WALK_SPEED_M_PER_SECOND);
        }
    }

    private Vector3 GetCharacterForward()
    {
        Vector3 forward = _innerCharacterTransform.forward;
        forward.y = 0;
        return forward;
    }
}
