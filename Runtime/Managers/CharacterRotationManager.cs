

using System.Collections;
using UnityEngine;

public class CharacterRotationManager : MonoBehaviour
{
    float ROTATION_SPEED = 10f;
    float ANGLE_THRESHOLD = 0.01f;
    float _verticalRotation = 0f;
    float _horizontalRotation = 0f;
    Quaternion _desiredRotation;

    [SerializeField]
    Transform _characterBodyTransform;
    public float verticalRotation
    {
        get
        {
            return _verticalRotation;
        }
        set
        {
            if (value > 180f) value -= 360f;
            _verticalRotation = Mathf.Clamp(value, -90f, 90f);
            SetDesiredRotation();
        }
    }
    public float horizontalRotation {
        get
        {
            return _horizontalRotation;
        }
        set
        {
            _horizontalRotation = value;
            SetDesiredRotation();
        }
    }

    void Update()
    {
        if (ShouldRotate())
        {
            Rotate();
        }
    }
    void SetDesiredRotation ()
    {
            _desiredRotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0);
    }

    bool ShouldRotate()
    {
        return Quaternion.Angle(_desiredRotation, _characterBodyTransform.rotation) > ANGLE_THRESHOLD;
    }

    void Rotate()
    {
        _characterBodyTransform.rotation = Quaternion.Slerp(_characterBodyTransform.rotation, _desiredRotation, ROTATION_SPEED * Time.deltaTime);
    }
}
