using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using System;

using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.models;
using ReupVirtualTwin.dataModels;

public class SetMaterialRequestsReceiverTest : MonoBehaviour
{
    GameObject ObjectRegistryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.reup.romulo/Assets/ScriptHolders/ObjectRegistry.prefab");
    GameObject objectRegistryGameObject, testObj0, testObj1, requestReceiverGameObject;
    SetMaterialRequestsReceiver requestReceiver;
    Texture2D testTexture;

    [SetUp]
    public void SetUp()
    {
        testTexture = new Texture2D(100,100);
        requestReceiverGameObject = new GameObject();
        requestReceiver = requestReceiverGameObject.AddComponent<SetMaterialRequestsReceiver>(); 
        requestReceiver.webRequestTexture = new MockWebRequestTexture(testTexture);
        objectRegistryGameObject = (GameObject)PrefabUtility.InstantiatePrefab(ObjectRegistryPrefab);
        testObj0 = new GameObject("testObj0");
        SetObject(testObj0);
        testObj1 = new GameObject("testObj1");
        SetObject(testObj1);
    }

    void SetObject(GameObject obj)
    {
        var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        obj.AddComponent<RegisteredIdentifier>();
        var renderer = obj.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
    }

    [TearDown]
    public void TearDown()
    {
        Destroy(requestReceiverGameObject);
        Destroy(testObj0);
        Destroy(testObj1);
        Destroy(objectRegistryGameObject);
    }

    [UnityTest]
    public IEnumerator ObjectsShouldChangeToNewMaterial()
    {
        Assert.AreNotEqual(testObj0.GetComponent<Renderer>().sharedMaterial.GetTexture("_BaseMap"), testTexture);
        Assert.AreNotEqual(testObj1.GetComponent<Renderer>().sharedMaterial.GetTexture("_BaseMap"), testTexture);

        var id0 = testObj0.GetComponent<RegisteredIdentifier>().getId();
        var id1 = testObj1.GetComponent<RegisteredIdentifier>().getId();

        SetMaterialRequest request = new SetMaterialRequest
        {
            objectsIds = new string[] { id0, id1 },
            submeshIndexes = new int[] { 0, 0},
            texturesUrl = "http://a-texture.url.png"
        };
        var serializedRequest = JsonUtility.ToJson(request);
        yield return requestReceiver.ReceiveSetMaterialRequest(serializedRequest);

        Assert.AreEqual(testObj0.GetComponent<Renderer>().sharedMaterial.GetTexture("_BaseMap"), testTexture);
        Assert.AreEqual(testObj1.GetComponent<Renderer>().sharedMaterial.GetTexture("_BaseMap"), testTexture);

        yield return null;
    }
}

public class MockWebRequestTexture : IWebRequestTexture
{
    Texture2D returnTexture;
    public MockWebRequestTexture(Texture2D texture)
    {
        returnTexture = texture;
    }
    public IEnumerator GetTexture(string url, Action<Texture2D> onSuccess, Action<string> onError)
    {
        yield return null;
        onSuccess?.Invoke(returnTexture);
    }
}
