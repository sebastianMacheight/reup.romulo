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

public class DeleteObjectsManagerTest : MonoBehaviour
{
    GameObject containerGameObject;
    GameObject runtimeDeleteObj;
    GameObject deleteWrapper;
    DeleteObjectsManager deleteObjectsManager;
    MockMediator mockMediator;
    GameObject deletableObject0;
    GameObject deletableObject1;
    GameObject nonDeletableObject;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject("containerGameObject");
        deleteWrapper = new GameObject("deleteWrapper");
        deleteObjectsManager = containerGameObject.AddComponent<DeleteObjectsManager>();
        deleteObjectsManager.tagsController = new TagsController();
        mockMediator = new MockMediator();
        deleteObjectsManager.mediator = mockMediator;
        deletableObject0 = new GameObject("deletableObject0");
        deletableObject0.AddComponent<ObjectTags>().AddTags(new ObjectTag[2] { ObjectTag.SELECTABLE, ObjectTag.DELETABLE });
        deletableObject1 = new GameObject("deletableObject1");
        deletableObject1.AddComponent<ObjectTags>().AddTags(new ObjectTag[2] { ObjectTag.SELECTABLE, ObjectTag.DELETABLE });
        nonDeletableObject = new GameObject("nonDeletableObject");
        nonDeletableObject.AddComponent<ObjectTags>().AddTags(new ObjectTag[1] { ObjectTag.SELECTABLE });
    }

    [UnityTest]
    public IEnumerator ShouldReturnTrueWhenSelectedObjectsAreDeletable()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { deletableObject0, deletableObject1 },
        };
        Assert.IsTrue(deleteObjectsManager.AreWrappedObjectsDeletable(objectWrapperDTO));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldDeleteDeletableObjects()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { deletableObject0, deletableObject1 },
        };
        deleteObjectsManager.DeleteSelectedObjects(objectWrapperDTO.wrappedObjects);
        Assert.IsTrue(objectWrapperDTO.wrappedObjects.All(obj => obj == null));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldFailWhenAttemptedToDeleteObjectButNoObjectIsSelected()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = null,
        };
        Assert.IsFalse(deleteObjectsManager.AreWrappedObjectsDeletable(objectWrapperDTO));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldFailWhenTryingToDeleteNonDeletableObjects()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { deletableObject0, deletableObject1, nonDeletableObject},
        };
        Assert.IsFalse(deleteObjectsManager.AreWrappedObjectsDeletable(objectWrapperDTO));
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldNotDeleteIfNoSelectedObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { },
        };
        Assert.IsFalse(deleteObjectsManager.AreWrappedObjectsDeletable(objectWrapperDTO));
        yield return null;
    }

    private class MockMediator : IMediator
    {
        public bool deleteModeActive = false;
        public bool notified = false;

        public void Notify(ReupEvent eventName)
        {
            if (eventName == ReupEvent.objectsDeleted){
                notified = true;
            }
        }

        public void Notify<T>(ReupEvent eventName, T payload)
        {
            throw new System.NotImplementedException();
        }
    }

}
