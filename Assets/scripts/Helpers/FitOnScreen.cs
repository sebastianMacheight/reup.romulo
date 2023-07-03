using UnityEngine;

public static class FitOnScreen
{

    public static Bounds GetBound(GameObject gameObj)
    {
        Bounds bound = new Bounds(gameObj.transform.position, Vector3.zero);
        var rList = gameObj.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer r in rList)
        {
            bound.Encapsulate(r.bounds);
        }
        return bound;
    }

    public static void Fit(GameObject gameObj, Transform characterTransform)
    {
        Bounds bound = GetBound(gameObj);
        //Vector3 boundSize = bound.size;
        //float diagonal = Mathf.Sqrt((boundSize.x * boundSize.x) + (boundSize.y * boundSize.y) + (boundSize.z * boundSize.z)); //Get box diagonal
        //Camera.main.orthographicSize = diagonal / 2.0f;
        //Camera.main.transform.position = bound.center;
        characterTransform.position = bound.center;
    }
}

