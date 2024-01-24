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
    public IEnumerator ShouldActivateAndDeactivateDeleteMode()
    {
        Assert.IsFalse(mockMediator.deleteModeActive);
        yield return null;
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = deleteWrapper,
            wrappedObjects = new List<GameObject>() { }
        };
        deleteObjectsManager.ActivateDeleteMode(objectWrapperDTO);
        Assert.IsTrue(mockMediator.deleteModeActive);
        yield return null;
        deleteObjectsManager.DeactivateDeleteMode();
        Assert.IsFalse(mockMediator.deleteModeActive);
        yield return null;
    }



    [UnityTest]
    public IEnumerator ShouldRaiseExceptionIfAttemptedToDeactivateDeleteModeButNoModeIsActiveToBeginWith()
    {
        Assert.AreEqual(false, mockMediator.notified);
        yield return null;
        Assert.That(() => deleteObjectsManager.DeactivateDeleteMode(),
            Throws.TypeOf<InvalidOperationException>()
        );
        Assert.AreEqual(false, mockMediator.notified);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldFailWhenAttemptedToActivateAnyModeButNoObjectIsSelected()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = null,
        };
        Assert.That(() => deleteObjectsManager.ActivateDeleteMode(objectWrapperDTO),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => deleteObjectsManager.ActivateDeleteMode(null),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldAllowToActivateDeleteModeWhenSelectedOnlyDeletableObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { deletableObject0, deletableObject1 },
        };
        deleteObjectsManager.ActivateDeleteMode(objectWrapperDTO);
        Assert.IsTrue(mockMediator.deleteModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldNotActivateDeleteModeIfAttemptedToDeleteNotDeletableObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { deletableObject0, deletableObject1, nonDeletableObject }
        };
        Assert.That(() => deleteObjectsManager.ActivateDeleteMode(objectWrapperDTO),
            Throws.TypeOf<ArgumentException>()
        );
        Assert.IsFalse(mockMediator.deleteModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldDeleteSelectedObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { deletableObject0, deletableObject1 },
        };
        deleteObjectsManager.ActivateDeleteMode(objectWrapperDTO);
        deleteObjectsManager.DeleteSelectedObjects(objectWrapperDTO);
        Assert.IsTrue(objectWrapperDTO.wrappedObjects.All(obj => obj == null));
        yield return null;
    }

    private class MockMediator : IMediator
    {
        public bool deleteModeActive = false;
        public bool notified = false;

        public void Notify(Events eventName)
        {
            switch (eventName)
            {
                case Events.deleteObjectsActivated:
                    deleteModeActive = true;
                    notified = true;
                    break;
                case Events.deleteObjectsDeactivated:
                    deleteModeActive = false;
                    notified = true;
                    break;
            }
        }

        public void Notify<T>(Events eventName, T payload)
        {
            throw new System.NotImplementedException();
        }
    }

}
