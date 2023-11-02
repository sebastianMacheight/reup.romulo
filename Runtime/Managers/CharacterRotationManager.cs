using UnityEngine;

public class CharacterRotationManager : MonoBehaviour
{
    float ROTATION_SPEED = 10f;
    float ANGLE_THRESHOLD = 0.01f;
    float _verticalRotation = 0f;
    float _horizontalRotation = 0f;
    Quaternion _desiredBodyRotation;
    Quaternion _desiredHorizontalRotation;

    [SerializeField]
    Transform _bodyTransform;
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
            SetDesiredBodyRotation();
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
            SetDesiredHorizontalRotation();
        }
    }

    private void Start()
    {
        verticalRotation = transform.rotation.eulerAngles.x;
        horizontalRotation = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        if (ShouldRotate())
        {
            Rotate();
        }
    }
    void SetDesiredHorizontalRotation ()
    {
        _desiredHorizontalRotation = Quaternion.Euler(0, _horizontalRotation, 0);
    }
    void SetDesiredBodyRotation()
    {
        _desiredBodyRotation = Quaternion.Euler(_verticalRotation, transform.rotation.eulerAngles.y, 0);
    }

    bool ShouldRotate()
    {
        var shouldRotateVertically = Quaternion.Angle(_desiredBodyRotation, _bodyTransform.rotation) > ANGLE_THRESHOLD;
        var shouldRotateHorizontally = Quaternion.Angle(_desiredHorizontalRotation, transform.rotation) > ANGLE_THRESHOLD;
        return shouldRotateVertically || shouldRotateHorizontally;
    }

    void Rotate()
    {
        var rotationStep = ROTATION_SPEED * Time.deltaTime;
        RotateCharacter(rotationStep);
        RotateBody(rotationStep);
    }
    void RotateBody(float rotationStep)
    {
        SetDesiredBodyRotation();
        _bodyTransform.rotation = Quaternion.Slerp(_bodyTransform.rotation, _desiredBodyRotation, rotationStep);
    }
    void RotateCharacter(float rotationStep)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredHorizontalRotation, rotationStep);
    }
}
