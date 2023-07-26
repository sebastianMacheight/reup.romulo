using UnityEngine;
public class AllObjectsSelector : Selector, IObjectSelector
{
    public GameObject CheckSelection(Ray ray)
    { 
	    RaycastHit hit;
	    bool didHit = CastRay(ray, out hit);
        if (didHit)
        {
            GameObject obj = hit.collider.gameObject;
            return obj;
        }
        return null;
	}
}

