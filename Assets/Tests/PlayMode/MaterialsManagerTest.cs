using System.Collections;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections.Generic;

public class MaterialsManagerTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator ChangeMaterialOfOneObjectShouldSuccess()
    {
        var obj0 = new GameObject();
        obj0.AddComponent<MeshRenderer>();

        var materialToSelect = new Material(Shader.Find("Standard"));

        var materialsManager = new GameObject().AddComponent<MaterialsManager>();

        materialsManager.SelectObjects(new List<GameObject>() { obj0 }, new int[1] { 0 });

        Assert.AreNotEqual(materialToSelect, obj0.GetComponent<Renderer>().sharedMaterial);

        materialsManager.SetNewMaterial(materialToSelect);
        Assert.AreEqual(materialToSelect, obj0.GetComponent<Renderer>().sharedMaterial);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ChangeMaterialOfSeveralObjectsShouldSuccess()
    {
        var obj0 = new GameObject();
        obj0.AddComponent<MeshRenderer>();
        var obj1 = new GameObject();
        obj1.AddComponent<MeshRenderer>();

        var materialToSelect = new Material(Shader.Find("Standard"));

        var materialsManager = new GameObject().AddComponent<MaterialsManager>();

        var objectsList = new List<GameObject>() { obj0, obj1 };

        materialsManager.SelectObjects(objectsList, new int[2] { 0, 0 });

        for (int i = 0; i < objectsList.Count; i++)
        {
            Assert.AreNotEqual(materialToSelect, objectsList[i].GetComponent<Renderer>().sharedMaterial);
        }

        materialsManager.SetNewMaterial(materialToSelect);
        for (int i = 0; i < objectsList.Count; i++)
        {
            Assert.AreEqual(materialToSelect, objectsList[i].GetComponent<Renderer>().sharedMaterial);
        }

        yield return null;
    }
}
