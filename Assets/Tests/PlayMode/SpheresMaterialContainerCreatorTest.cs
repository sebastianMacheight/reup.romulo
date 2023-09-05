using NUnit.Framework;
using ReupVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class SpheresMaterialContainerCreatorTest
{
    private GameObject poolObject;
    private ObjectPool pool;
    private GameObject containerPrefab;
    private GameObject spherePrefab;
    private Camera camera;
    private SpheresMaterialContainerCreator creator;
    [SetUp]
    public void SetUp()
    {
        // Create object pool
        poolObject = new GameObject();
        poolObject.tag = TagsEnum.objectPool;
        pool = poolObject.AddComponent<ObjectPool>();
        containerPrefab = new GameObject("container");
        spherePrefab = new GameObject("sphere");
        spherePrefab.AddComponent<MeshRenderer>();
        pool.PrefabsForPool = new List<GameObject>
        {
            containerPrefab, spherePrefab
        };

        // Create main camera
        camera = new GameObject().AddComponent<Camera>();
        camera.tag = TagsEnum.mainCamera;

        // Create container creator
        creator = new GameObject().AddComponent<SpheresMaterialContainerCreator>();
        creator.materialsContainerPrefab = containerPrefab;
        creator.materialsSpherePrefab = spherePrefab;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(poolObject);
        Object.Destroy(containerPrefab);
        Object.Destroy(spherePrefab);
        Object.Destroy(camera);
        Object.Destroy(creator);
    }

    [UnityTest]
    public IEnumerator CreateContainer_should_success()
    {

        var materials = new Material[3]
        {
            new Material(Shader.Find("Standard")),
            new Material(Shader.Find("Standard")),
            new Material(Shader.Find("Standard")),
        };

        // Check there is no container
        Assert.AreEqual(0, camera.transform.childCount);

        // Create cointainer
        var container = creator.CreateContainer(materials);

        // Check container is a child of camera
        Assert.AreEqual(camera.gameObject, container.transform.parent.gameObject);

        // Check the material spheres are there
        Assert.AreEqual(materials.Length, container.transform.childCount);

        // Check the materials of the spherematerials
        for (int i = 0; i < materials.Length; i++)
        {
            var sphereMaterial = container.transform.GetChild(i);
            Assert.AreEqual(materials[i], sphereMaterial.GetComponent<Renderer>().sharedMaterial);
        }


        yield return null;
    }
}
