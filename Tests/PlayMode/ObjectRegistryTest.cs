using System.Collections;
using NUnit.Framework;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectRegistryTest : MonoBehaviour
{
    GameObject ObjectRegistryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.reup.romulo/Assets/ScriptHolders/ObjectRegistry.prefab");
    GameObject objectRegistryGameObject;
    ObjectRegistry objectRegistry;
    GameObject testObj;

    [SetUp]
    public void SetUp()
    {
        objectRegistryGameObject = (GameObject)PrefabUtility.InstantiatePrefab(ObjectRegistryPrefab);
        testObj = new GameObject("testObj");
        testObj.AddComponent<RegisteredIdentifier>();
        objectRegistry = ObjectFinder.FindObjectRegistry().GetComponent<ObjectRegistry>();
    }

    [TearDown]
    public void TearDown()
    {
        Destroy(testObj);
        Destroy(objectRegistryGameObject);
    }

    [UnityTest]
    public IEnumerator TestObjHasAnId()
    {
        var id = testObj.GetComponent<RegisteredIdentifier>().getId();
        Assert.IsNotNull(id);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ObjectRegistryContainsTestObj()
    {
        var id = testObj.GetComponent<RegisteredIdentifier>().getId();
        var obtainedObj = objectRegistry.GetItemWithGuid(id);
        Assert.AreEqual(testObj, obtainedObj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoObjectIsReturnedIfIncorrectId()
    {
        var obtainedObj = objectRegistry.GetItemWithGuid("an-incorrect-id");
        Assert.AreEqual(obtainedObj, null);
        yield return null;
    }
}
