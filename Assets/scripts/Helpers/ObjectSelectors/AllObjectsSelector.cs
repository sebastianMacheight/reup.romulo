using UnityEngine;
public class AllObjectsSelector : MonoBehaviour, IObjectSelector
{
    public GameObject CheckSelection(Ray ray)
    { 
	    RaycastHit hit;
	    bool didHit = Physics.Raycast(ray, out hit);
        if (didHit)
        {
            GameObject obj = hit.collider.gameObject;
            return obj;
        }
        return null;
	}
}

