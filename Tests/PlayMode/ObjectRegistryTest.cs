using System.Collections;
using NUnit.Framework;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectRegistryTest : MonoBehaviour
{
    GameObject testObj;
    GameObject objectRegistryGameObject;
    ObjectRegistry objectRegistry;

    [SetUp]
    public void SetUp()
    {
        objectRegistryGameObject = new GameObject("ObjectRegistry");
        objectRegistryGameObject.tag = "ObjectRegistry";
        objectRegistryGameObject.AddComponent<ObjectRegistry>();
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
        var obtainedObj = objectRegistry.GetObjectWithGuid(id);
        Assert.AreEqual(testObj, obtainedObj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoObjectIsReturnedIfIncorrectId()
    {
        var obtainedObj = objectRegistry.GetObjectWithGuid("an-incorrect-id");
        Assert.AreEqual(obtainedObj, null);
        yield return null;
    }
}
