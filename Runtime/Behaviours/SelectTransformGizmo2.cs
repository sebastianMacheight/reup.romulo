using UnityEngine;
using UnityEngine.EventSystems;
using RuntimeHandle;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.enums;

public class SelectTransformGizmo2 : MonoBehaviour
{
    
    private Transform selection;
    private RaycastHit raycastHit;
    private RaycastHit raycastHitHandle; //This is actually never used, but eeeh
    private GameObject runtimeTransformGameObj;
    private RuntimeTransformHandle runtimeTransformHandle;
    private int runtimeTransformLayer = 6;
    private int runtimeTransformLayerMask;
    private ObjectWrapper objectWrapper;

    private void Start()
    {
        runtimeTransformGameObj = new GameObject();
        runtimeTransformHandle = runtimeTransformGameObj.AddComponent<RuntimeTransformHandle>();
        runtimeTransformGameObj.layer = runtimeTransformLayer;
        runtimeTransformLayerMask = 1 << runtimeTransformLayer; //Layer number represented by a single bit in the 32-bit integer using bit shift
        runtimeTransformHandle.type = HandleType.POSITION;
        runtimeTransformHandle.autoScale = true;
        runtimeTransformHandle.autoScaleFactor = 1.0f;
        runtimeTransformGameObj.SetActive(false);
        objectWrapper = new ObjectWrapper();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ApplyLayerToChildren(runtimeTransformGameObj);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (Physics.Raycast(ray, out raycastHitHandle, Mathf.Infinity, runtimeTransformLayerMask)) //Raycast towards runtime transform handle only
                {
                    //We are handleling the transform handle. Don't do anything
                }
                else
                {
                    Transform hitObject = FindFirstSelectable(raycastHit.transform);
                    if (hitObject != null)
                    {
                        selection = objectWrapper.WrapObject(hitObject.gameObject).transform;
                        runtimeTransformHandle.target = selection;
                        runtimeTransformGameObj.SetActive(true);
                    }
                    else
                    {
                        Deselect();
                    }
                }
            }
            else
            {
                if (selection)
                {
                    Deselect();
                }
            }
        }

        //Hot Keys for move, rotate, scale, local and Global/World transform
        if (runtimeTransformGameObj.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                runtimeTransformHandle.type = HandleType.POSITION;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                runtimeTransformHandle.type = HandleType.ROTATION;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                runtimeTransformHandle.type = HandleType.SCALE;
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    runtimeTransformHandle.space = HandleSpace.WORLD;
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    runtimeTransformHandle.space = HandleSpace.LOCAL;
                }
            }
        }
    }

    private void Deselect()
    {
        selection = null;
        objectWrapper.DeWrapAll();
        runtimeTransformGameObj.SetActive(false);
    }

    public Transform FindFirstSelectable(Transform transform)
    {
        if (transform.CompareTag(TagsEnum.selectableObject))
        {
            return transform;
        }
        if (transform.parent == null)
        {
            return null;
        }
        return FindFirstSelectable(transform.parent);
    }

    private void ApplyLayerToChildren(GameObject parentGameObj)
    {
        foreach (Transform transform1 in parentGameObj.transform)
        {
            int layer = parentGameObj.layer;
            transform1.gameObject.layer = layer;
            foreach (Transform transform2 in transform1)
            {
                transform2.gameObject.layer = layer;
                foreach (Transform transform3 in transform2)
                {
                    transform3.gameObject.layer = layer;
                    foreach (Transform transform4 in transform3)
                    {
                        transform4.gameObject.layer = layer;
                        foreach (Transform transform5 in transform4)
                        {
                            transform5.gameObject.layer = layer;
                        }
                    }
                }
            }
        }
    }

}