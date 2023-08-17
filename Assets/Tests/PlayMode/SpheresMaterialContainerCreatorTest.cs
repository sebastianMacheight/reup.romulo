using NUnit.Framework;
using ReupVirtualTwin.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class SpheresMaterialContainerCreatorTest
{
    [UnityTest]
    public IEnumerator CreateContainer_should_success()
    {
        // Create object pool
        var poolObject = new GameObject();
        poolObject.tag = TagsEnum.objectPool;
        var pool = poolObject.AddComponent<ObjectPool>();
        var containerPrefab = new GameObject("container");
        var spherePrefab = new GameObject("sphere");
        spherePrefab.AddComponent<MeshRenderer>();
        pool.PrefabsForPool = new List<GameObject>
        {
            containerPrefab, spherePrefab
        };

        // Create main camera
        var camera = new GameObject().AddComponent<Camera>();
        camera.tag = TagsEnum.mainCamera;

        // Create container creator
        var creator = new GameObject().AddComponent<SpheresMaterialContainerCreator>();
        creator.materialsContainerPrefab = containerPrefab;
        creator.materialsSpherePrefab = spherePrefab;

        var materials = new Material[3]
        {
            new Material(Shader.Find("Standard")),
            new Material(Shader.Find("Standard")),
            new Material(Shader.Find("Standard")),
        };

        materials[0].name = "material0";
        materials[1].name = "material1";
        materials[2].name = "material2";

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
