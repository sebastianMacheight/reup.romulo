using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositionManager : MonoBehaviour
{
    [SerializeField]
    private float characterVelocity = 3f;
    [SerializeField]
    private float floorDistanceThreshold = 0.7f;

    private float _stopDistance;
    private float stopDistance {
        get
        {
            return _stopDistance;
        }
        set
        {
            if (value < 0.01f)
            {
                _stopDistance = 0.01f;
            }
            else
            {
                _stopDistance = value;
            }
        } }

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
        StopCoroutine("MoveToTargetCoroutine");
        var step = direction * Time.deltaTime * characterVelocity;
        transform.position += step;
    }
    public void WalkToTarget(Vector3 target)
    {
        stopDistance = (target.y > _characterPosition.y - floorDistanceThreshold) ? 0.5f : 0.05f;
        target.y = _characterPosition.y;
        MoveToTarget(target);
    }
    public void SliceToTarget(Vector3 target)
    {
        stopDistance = 0;
        MoveToTarget(target);
    }

    public void MoveToTarget(Vector3 target)
    {
        StopCoroutine("MoveToTargetCoroutine");
        StartCoroutine("MoveToTargetCoroutine", target);
    }

    private IEnumerator MoveToTargetCoroutine(Vector3 target)
    {
        while(Vector3.Distance(target, _characterPosition) > stopDistance)
        {
            _characterPosition = Vector3.Lerp(_characterPosition, target, characterVelocity * Time.deltaTime);
            yield return null;
        }
    }

}
