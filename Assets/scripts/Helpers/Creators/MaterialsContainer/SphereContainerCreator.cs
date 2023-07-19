using ReUpVirtualTwin.Helpers;
using System;
using UnityEngine;

public class SphereContainerCreator : MaterialsContainerCreator
{
    //public Material[] testMaterialsList; // todo obtain the material list from the object hit by the raycast
    public GameObject materialsContainerPrefab;
    public GameObject materialsSpherePrefab;
    public float spheresZDistance = 0.14f;
    public float spheresYDistance = -0.06f;

    Camera _mainCamera;

    private IObjectPool _objectPool;

    private void Start()
    {
        _objectPool = ObjectFinder.FindObjectPool();
        _mainCamera = Camera.main;
    }

    public override void CreateContainer(GameObject obj, Material[] selectableMaterials)
    {
        // If materials container is showing, hide it instead
        if (materialsContainerInstance != null)
        {
            //HideContainer();
            Debug.Log("instance already exits");
            return;
        }
        materialsContainerInstance = _objectPool.GetObjectFromPool(materialsContainerPrefab.name, _mainCamera.transform);
        PlaceSpheresAroundCamera(selectableMaterials);
    }

    private float CameraHorizontalAngle()
    {
        float verticalRadAngle = _mainCamera.fieldOfView * Mathf.Deg2Rad;
        float horizontalRadAngle = (float)(2 * Math.Atan(Mathf.Tan(verticalRadAngle / 2) * _mainCamera.aspect));
        return horizontalRadAngle * Mathf.Rad2Deg;
	}

    public void PlaceSpheresAroundCamera(Material[] selectableMaterials)
    { 
        int materialsCount = selectableMaterials.Length;
        //get horizontal angle of camera
        float cameraOpenAngle = CameraHorizontalAngle();
        //calculate angle between each sphere
        float angleStepBetweenSphere = cameraOpenAngle / (materialsCount + 1);
        //calculate offsetAngle to make the spheres appear in the midle of the front of the camera
        float offsetAngle = cameraOpenAngle / 2;
        //calculate the position of the middle sphere if any
        Vector3 middleSpherePosition = new Vector3(0, spheresYDistance, spheresZDistance);
        //calculate an ortogonal axis to the middle sphere position to rotate all the spheres horizontally respect to this axis
        Vector3 rotateSpheresAngle = Quaternion.AngleAxis(90, new Vector3(1, 0, 0)) * middleSpherePosition;
        for (int i = 0; i < materialsCount; i++)
        {
            GameObject sphere = _objectPool.GetObjectFromPool(materialsSpherePrefab.name, materialsContainerInstance.transform);
            //calculate the angle to rotate the sphere respect to the middle position
            float sphereAngle = (i + 1) * angleStepBetweenSphere - offsetAngle;
            //rotate the sphere position respect to the ortogonal axis calculated previously
            sphere.transform.localPosition = Quaternion.AngleAxis(sphereAngle, rotateSpheresAngle) * middleSpherePosition;
            sphere.GetComponent<Renderer>().material = selectableMaterials[i];
        }
	}

    public override void HideContainer()
    {
        for (int i = 0; i < materialsContainerInstance.transform.childCount; i++)
        {
            GameObject sphere = materialsContainerInstance.transform.GetChild(i).gameObject;
            _objectPool.PoolObject(sphere);
        }
        _objectPool.PoolObject(materialsContainerInstance);
        materialsContainerInstance = null;
    }
}

