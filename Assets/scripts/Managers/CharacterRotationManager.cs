using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotationManager : MonoBehaviour
{
    public float verticalRotation { get; set; } = 0f;
    public float horizontalRotation {get; set;}

    private void Start()
    {
        horizontalRotation = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (verticalRotation > 180f) verticalRotation -= 360f;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
    }
}
