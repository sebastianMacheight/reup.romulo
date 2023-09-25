

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotationManager : MonoBehaviour
{
    public float verticalRotation { get; set; } = 0f;
    public float horizontalRotation { get; set; }

    public float smoothness = 10f;

    private Quaternion desiredRotation;

    private void Start()
    {
        horizontalRotation = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (verticalRotation > 180f) verticalRotation -= 360f;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        desiredRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }

    void LateUpdate()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothness * Time.deltaTime);
    }
}
