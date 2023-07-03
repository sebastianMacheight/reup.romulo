using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositionManager : MonoBehaviour
{
    [SerializeField]
    private float characterVelocity = 3f;
    [SerializeField]
    private float floorDistanceThreshold = 0.7f;

    Vector3 _characterPosition
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public void MovePositionByStepInDirection(Vector3 direction)
    {
        StopCoroutine("MoveToTarget");
        var step = direction * Time.deltaTime * characterVelocity;
        transform.position += step;
    }
    public void MovePositionToTarget(Vector3 target)
    {
        StopCoroutine("MoveToTarget");
        StartCoroutine("MoveToTarget", target);
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        float stopDistance = (target.y > _characterPosition.y - floorDistanceThreshold) ? 0.5f : 0.05f;
        target.y = _characterPosition.y;
        while(Vector3.Distance(target, _characterPosition) > stopDistance)
        {
            _characterPosition = Vector3.Lerp(_characterPosition, target, characterVelocity * Time.deltaTime);
            yield return null;
        }
    }

}
