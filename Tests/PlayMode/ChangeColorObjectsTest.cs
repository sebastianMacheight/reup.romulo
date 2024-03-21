using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.dataModels;
using System;
using ReupVirtualTwin.models;
using ReupVirtualTwin.controllers;
using NUnit.Framework;

public class ChangeColorObjectsTest : MonoBehaviour
{
    GameObject containerGameObject;
    GameObject changeColorWrapper;
    ChangeColorManager changeColorManager;
    MockMediator mockMediator;
    MockRegistry mockRegistry;
    public List<GameObject> allObjects = new List<GameObject>();

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject("containerGameObject");
        changeColorWrapper = new GameObject("changeColorWrapper");
        changeColorManager = containerGameObject.AddComponent<ChangeColorManager>();
        changeColorManager.tagsController = new TagsController();
        mockMediator = new MockMediator();
        mockRegistry = new MockRegistry();
        changeColorManager.mediator = mockMediator;
        changeColorManager.registry = mockRegistry;
        allObjects = mockRegistry.allObjects;
    }
    public List<string> GetIDsArray(List<GameObject> gameObjects)
    {
        List<string> stringIDs = new List<string>();
        foreach (GameObject obj in gameObjects)
        {
            stringIDs.Add(obj.GetComponent<UniqueId>().getId());
        }
        return stringIDs;
    }

    [UnityTest]
    public IEnumerator ShouldReturnArrayWithObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>() { allObjects[0], allObjects[1] };
        List<string> stringIDs = GetIDsArray(gameObjects);
        Assert.IsNotEmpty(changeColorManager.GetObjectsToChangeColor(stringIDs));
        yield return null;

    }

    [UnityTest]
    public IEnumerator ShouldReturnArrayWithObjectsAndChildren()
    {
        List<GameObject> gameObjects = new List<GameObject>() { allObjects[0], allObjects[1] };
        List<string> stringIDs = GetIDsArray(gameObjects);
        List<GameObject> objectsToChangeColor = changeColorManager.GetObjectsToChangeColor(stringIDs);
        int expectedCount = gameObjects.Count + gameObjects.Sum(obj => obj.transform.childCount);
        Assert.AreEqual(expectedCount, objectsToChangeColor.Count);
        yield return null;

    }

    [UnityTest]
    public IEnumerator ShouldReturnEmptyListWhenNull()
    {
        List<string> nullArray = null;
        Assert.IsEmpty(changeColorManager.GetObjectsToChangeColor(nullArray));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldReturnEmptyListWhenEmpty()
    {
        List<string> emptyArray = new();
        Assert.IsEmpty(changeColorManager.GetObjectsToChangeColor(emptyArray));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldParseStringToColor()
    {
        Color? parsedColor = changeColorManager.parseColor("#0000FF");
        Assert.IsInstanceOf<Color>(parsedColor);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldFailWhenStringIsNotHex()
    {
        Color? parsedColor = changeColorManager.parseColor("NotAnHex");
        Assert.IsNull(parsedColor);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldChangeColorInPaintableObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>() { allObjects[0], allObjects[1] };

        changeColorManager.ChangeColorObjects(gameObjects, Color.blue);

        yield return null;

        Renderer renderer0 = allObjects[0].GetComponent<Renderer>();
        Assert.AreEqual(renderer0.material.color, Color.blue);

        Renderer renderer1 = allObjects[1].GetComponent<Renderer>();
        Assert.AreEqual(renderer1.material.color, Color.blue);
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
    private class MockRegistry : IRegistry
    {
        public List<GameObject> allObjects = new List<GameObject>();
        public MockRegistry()
        {
            GameObject object0 = new GameObject("object0");
            object0.AddComponent<ObjectTags>().AddTags(new ObjectTag[1] { ObjectTag.SELECTABLE });
            object0.AddComponent<UniqueId>().GenerateId();
            object0.AddComponent<MeshRenderer>();

            GameObject object1 = new GameObject("object1");
            object1.AddComponent<ObjectTags>().AddTags(new ObjectTag[1] { ObjectTag.SELECTABLE });
            object1.AddComponent<UniqueId>().GenerateId();
            object1.AddComponent<MeshRenderer>();
            allObjects.Add(object0);
            allObjects.Add(object1);

            GameObject child1 = new GameObject("Child1");
            child1.AddComponent<ObjectTags>().AddTags(new ObjectTag[1] { ObjectTag.SELECTABLE });
            child1.AddComponent<UniqueId>().GenerateId();
            child1.AddComponent<MeshRenderer>();
            child1.transform.parent = object0.transform;

            GameObject child2 = new GameObject("Child2");
            child2.AddComponent<ObjectTags>().AddTags(new ObjectTag[1] { ObjectTag.SELECTABLE });
            child2.AddComponent<UniqueId>().GenerateId();
            child2.AddComponent<MeshRenderer>();
            child2.transform.parent = object1.transform;
        }

        public void AddItem(GameObject obj)
        {
            throw new NotImplementedException();
        }
        public GameObject GetItemWithGuid(string guid)
        {
            foreach (GameObject obj in allObjects)
            {
                if (obj == null) continue;
                var uniqueIdentifier = obj.GetComponent<UniqueId>();
                if (uniqueIdentifier.isIdCorrect(guid))
                {
                    return obj;
                }
            }
            return null;
        }
        public List<GameObject> GetItemsWithGuids(string[] guids)
        {
            var foundObjects = new List<GameObject>();
            foreach (string guid in guids)
            {
                foundObjects.Add(GetItemWithGuid(guid));
            }
            return foundObjects;
        }
    }
}
