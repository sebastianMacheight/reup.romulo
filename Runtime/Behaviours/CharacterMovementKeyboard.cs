using UnityEngine;
using ReupVirtualTwin.characterMovement;

public class CharacterMovementKeyboard : MonoBehaviour
{
    [SerializeField]
    private Transform _innerCharacterTransform;

    private InputProvider _inputProvider;
    [SerializeField]
    private CharacterPositionManager _characterPositionManager;
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
        Vector3 movementDirection = inputValue.x * GetCharacterRight() +
                            inputValue.y * GetCharacterForward();
        if (movementDirection != Vector3.zero && _characterPositionManager.allowWalking)
        {
            _characterPositionManager.StopWalking();
            _characterPositionManager.MoveInDirection(movementDirection, _walkSpeed);
        }
    }

    private Vector3 GetCharacterRight()
    {
        Vector3 right = _innerCharacterTransform.right;
        right.y = 0;
        return right;
    }
    private Vector3 GetCharacterForward()
    {
        Vector3 forward = _innerCharacterTransform.forward;
        forward.y = 0;
        return forward;
    }
}
