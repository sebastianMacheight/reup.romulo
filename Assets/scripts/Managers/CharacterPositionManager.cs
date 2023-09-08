using System.Collections;
using UnityEngine;

public class CharacterPositionManager : MonoBehaviour
{
    [SerializeField]
    private float movementForceMultiplier = 10f;
    [SerializeField]
    private float floorDistanceThreshold = 0.7f;
    private Rigidbody rb;
    [SerializeField]
    private float bodyDrag = 5f;

    float _stopDistance = 0.5f;


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

    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = bodyDrag;
    }

    public void MovePositionByStepInDirection(Vector3 direction)
    {
        StopCoroutine("MoveToTargetCoroutine");
        rb.isKinematic = false;
        var step = direction * movementForceMultiplier;
        rb.AddForce(step, ForceMode.Force);
    }
    public void WalkToTarget(Vector3 target)
    {
        target.y = _characterPosition.y;
        MoveToTarget(target);
    }
    public void SliceToTarget(Vector3 target)
    {
        //put target _stopdistance meters futher away
        var newTarget = target + Vector3.Normalize(target - _characterPosition) * _stopDistance;
        MoveToTarget(newTarget);
    }

    public void MoveToTarget(Vector3 target)
    {
        StopCoroutine("MoveToTargetCoroutine");
        StartCoroutine("MoveToTargetCoroutine", target);
    }

    private IEnumerator MoveToTargetCoroutine(Vector3 target)
    {
        rb.isKinematic = true;
        while(Vector3.Distance(target, _characterPosition) > _stopDistance)
        {
            _characterPosition = Vector3.Lerp(_characterPosition, target, movementForceMultiplier * Time.deltaTime / 10);
            yield return null;
        }
        rb.isKinematic = false;
    }
}

