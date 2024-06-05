using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using ReupVirtualTwin.models;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.helpers;
using Newtonsoft.Json.Linq;

public class ChangeColorObjectsTest : MonoBehaviour
{
    ReupSceneInstantiator.SceneObjects sceneObjects;
    ChangeColorManager changeColorManager;
    MockMediator mockMediator;

    GameObject meshedParent;
    GameObject unmeshedParent;
    GameObject unmeshedChild;
    GameObject meshedChild;

    [SetUp]
    public void SetUp()
    {
        sceneObjects = ReupSceneInstantiator.InstantiateScene();
        changeColorManager = sceneObjects.changeColorManager;
        mockMediator = new MockMediator();
        changeColorManager.mediator = mockMediator;
        CreateObjects();
    }
    [TearDown]
    public void TearDown()
    {
        ReupSceneInstantiator.DestroySceneObjects(sceneObjects);
        Destroy(meshedParent);
        Destroy(unmeshedParent);
        Destroy(meshedChild);
        Destroy(unmeshedChild);
    }
    private void CreateObjects()
    {
        meshedParent = new GameObject();
        unmeshedParent = new GameObject();
        unmeshedChild = new GameObject();
        meshedChild = new GameObject();

        meshedParent.AddComponent<RegisteredIdentifier>().GenerateId();
        meshedParent.AddComponent<MeshRenderer>();

        unmeshedParent.AddComponent<RegisteredIdentifier>().GenerateId();

        unmeshedChild.AddComponent<RegisteredIdentifier>().GenerateId();
        unmeshedChild.transform.parent = meshedParent.transform;

        meshedChild.AddComponent<RegisteredIdentifier>().GenerateId();
        meshedChild.AddComponent<MeshRenderer>();
        meshedChild.transform.parent = unmeshedParent.transform;
    }

    public List<string> GetIDsArray(List<GameObject> gameObjects)
    {
        List<string> stringIDs = new List<string>();
        foreach (GameObject obj in gameObjects)
        {
            stringIDs.Add(obj.GetComponent<IUniqueIdentifier>().getId());
        }
        return stringIDs;
    }
    private class MockMediator : IMediator
    {
        public bool deleteModeActive = false;
        public bool notified = false;

        public void Notify(ReupEvent eventName)
        {
            if (eventName == ReupEvent.objectsDeleted)
            {
                notified = true;
            }
        }

        public void Notify<T>(ReupEvent eventName, T payload)
        {
            throw new System.NotImplementedException();
        }
    }

    [UnityTest]
    public IEnumerator ShouldNotComplain_When_TryingToChangeColorOfUnmeshedObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>() { unmeshedParent, unmeshedChild };
        changeColorManager.ChangeObjectsColor(gameObjects, Color.blue);
        yield return null;
    }


    [UnityTest]
    public IEnumerator ShouldChangeColorObjects()
    {
        List<GameObject> gameObjects = new() { meshedParent, meshedChild, unmeshedParent, unmeshedChild};
        changeColorManager.ChangeObjectsColor(gameObjects, Color.blue);

        yield return null;

        Assert.AreEqual(Color.blue, meshedParent.GetComponent<Renderer>().material.color);
        Assert.AreEqual(Color.blue, meshedChild.GetComponent<Renderer>().material.color);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldAddColorMetaDataToObjects()
    {
        List<GameObject> gameObjects = new() { meshedParent, meshedChild, unmeshedParent, unmeshedChild};
        changeColorManager.ChangeObjectsColor(gameObjects, Color.blue);
        yield return null;

        string blueColorRGBA = ColorUtility.ToHtmlStringRGBA(Color.blue);
        Assert.AreEqual(blueColorRGBA, ObjectMetaDataUtils.GetObjectColor(meshedParent));
        Assert.AreEqual(blueColorRGBA, ObjectMetaDataUtils.GetObjectColor(meshedChild));
        Assert.AreEqual(null, ObjectMetaDataUtils.GetObjectColor(unmeshedParent));
        Assert.AreEqual(null, ObjectMetaDataUtils.GetObjectColor(unmeshedChild));
        yield return null;

        changeColorManager.ChangeObjectsColor(gameObjects, Color.red);
        yield return null;

        string redColorRGBA = ColorUtility.ToHtmlStringRGBA(Color.red);
        Assert.AreEqual(redColorRGBA, ObjectMetaDataUtils.GetObjectColor(meshedParent));
        Assert.AreEqual(redColorRGBA, ObjectMetaDataUtils.GetObjectColor(meshedChild));
        Assert.AreEqual(null, ObjectMetaDataUtils.GetObjectColor(unmeshedParent));
        Assert.AreEqual(null, ObjectMetaDataUtils.GetObjectColor(unmeshedChild));
        yield return null;
    }

}
