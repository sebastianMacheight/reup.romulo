using ReUpVirtualTwin;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IRayCastHitSelector))]
public abstract class SelectPoint : Select 
{
    private IRayCastHitSelector _hitSelector; 

    protected override void Awake()
    {
        base.Awake();
        _hitSelector = GetComponent<IRayCastHitSelector>();
    }

    public override void OnSelect(InputAction.CallbackContext ctx)
    {
        if (!_dragManager.prevDragging && !_dragManager.pointerUnderUi)
        {
            Ray ray = _rayProvider.GetRay();
            //Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);
            RaycastHit? hit = _hitSelector.GetHit(ray);
		    if (hit !=null) 
		    {
                HandleHit((RaycastHit)hit);
		    }
            else
            {
                MissHit();
            }
        }
    }
    public abstract void HandleHit(RaycastHit hit);
    public virtual void MissHit() { }
}
