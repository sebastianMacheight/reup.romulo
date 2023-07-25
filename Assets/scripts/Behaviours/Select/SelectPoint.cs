using ReUpVirtualTwin;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using ReUpVirtualTwin.Helpers;

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
        if (!_dragManager.prevDragging && !OverUICheck.PointerOverUI2() && !_dragManager.prevSelectInputInUI)
        {
            Ray ray = _rayProvider.GetRay();
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
