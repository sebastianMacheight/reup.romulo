using UnityEngine;
using UnityEngine.InputSystem;



namespace ReUpVirtualTwin
{

[RequireComponent(typeof(IObjectSelector))]
public abstract class SelectObject : Select 
{
    private IObjectSelector _objectSelector; 

    protected override void Awake()
    {
        base.Awake();
        _objectSelector = GetComponent<IObjectSelector>();
    }

    public override void OnSelect(InputAction.CallbackContext ctx)
    {
        //if (!_dragManager.prevDragging && !_dragManager.pointerUnderUi && !_dragManager.prevSelectInputInUI)
        if (!_dragManager.prevDragging && !_dragManager.prevSelectInputInUI)
        {
            Ray ray = _rayProvider.GetRay();
            //Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);
            GameObject obj = _objectSelector.CheckSelection(ray);
		    if (obj !=null) 
		    {
                HandleObject(obj);
		    }
            else
            {
                MissObject();
            }
        }
    }
    public abstract void HandleObject(GameObject obj);
    public virtual void MissObject() { }
}
}