using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Linq;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.dataModels;
using System;
using ReupVirtualTwin.models;
using ReupVirtualTwin.controllers;

public class ChangeColorObjectsTest : MonoBehaviour
{
    GameObject containerGameObject;
    //GameObject runtimeDeleteObj;
    GameObject changeColorWrapper;
    ChangeColorManager changeColorManager;
    MockMediator mockMediator;
    GameObject paintableObject0;
    GameObject paintableObject1;
    GameObject nonPaintableObject;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject("containerGameObject");
        changeColorWrapper = new GameObject("changeColorWrapper");
        changeColorManager = containerGameObject.AddComponent<ChangeColorManager>();
        changeColorManager.tagsController = new TagsController();
        mockMediator = new MockMediator();
        changeColorManager.mediator = mockMediator;
        paintableObject0 = new GameObject("paintableObject0");
        paintableObject0.AddComponent<ObjectTags>().AddTags(new ObjectTag[2] { ObjectTag.SELECTABLE, ObjectTag.PAINTABLE });
        paintableObject1 = new GameObject("paintableObject1");
        paintableObject1.AddComponent<ObjectTags>().AddTags(new ObjectTag[2] { ObjectTag.SELECTABLE, ObjectTag.PAINTABLE });
        nonPaintableObject = new GameObject("nonPaintableObject");
        nonPaintableObject.AddComponent<ObjectTags>().AddTags(new ObjectTag[1] { ObjectTag.SELECTABLE });
    }

    [UnityTest]
    public IEnumerator ShouldReturnTrueWhenSelectedObjectsArePaintable()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { paintableObject0, paintableObject1 },
        };
        Assert.IsTrue(changeColorManager.AreWrappedObjectsPaintable(objectWrapperDTO));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldFailWhenAttemptedToChangeColorButNoObjectIsSelected()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = null,
        };
        Assert.IsFalse(changeColorManager.AreWrappedObjectsPaintable(objectWrapperDTO));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldFailWhenTryingToChangeColorOnNonPaintableObjects()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { paintableObject0, paintableObject1, nonPaintableObject },
        };
        Assert.IsFalse(changeColorManager.AreWrappedObjectsPaintable(objectWrapperDTO));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldFailIfNoSelectedObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { },
        };
        Assert.IsFalse(changeColorManager.AreWrappedObjectsPaintable(objectWrapperDTO));
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
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { paintableObject0, paintableObject1 },
        };

        changeColorManager.ChangeColorSelectedObjects(objectWrapperDTO.wrappedObjects, "#0000FF");

        yield return null;

        Renderer renderer0 = paintableObject0.GetComponent<Renderer>();
        Assert.AreEqual(renderer0.material.color, Color.blue); 

        Renderer renderer1 = paintableObject1.GetComponent<Renderer>();
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
}
