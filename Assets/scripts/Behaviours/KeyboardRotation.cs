using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.2f;

    private InputProvider _inputProvider;
    private CharacterRotationManager _characterRotationManager;

    private void Awake()
    {
        _inputProvider = new InputProvider();
        _characterRotationManager = GetComponent<CharacterRotationManager>();
    }

    void Update()
    {
        Vector2 input = _inputProvider.RotateViewKeyboardInput();
		float horizontalInput = input.x;
        float verticalInput = input.y;

        _characterRotationManager.horizontalRotation += horizontalInput * rotationSpeed;
        _characterRotationManager.verticalRotation -= verticalInput * rotationSpeed;
    }
}
