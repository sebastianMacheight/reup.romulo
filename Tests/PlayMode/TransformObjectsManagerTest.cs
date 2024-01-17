using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.dataModels;
using System;

public class TransformObjectsManagerTest : MonoBehaviour
{
    GameObject containerGameObject;
    GameObject runtimeTransformObj;
    GameObject transformWrapper;
    TransformObjectsManager transformObjectsManager;
    MockMediator mockMediator;
    GameObject transformableObject0;
    GameObject transformableObject1;
    GameObject nonTransformableObject;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject("containerGameObject");
        transformWrapper = new GameObject("transformWrapper");
        transformObjectsManager = containerGameObject.AddComponent<TransformObjectsManager>();
        runtimeTransformObj = new GameObject("TransformHandle");
        runtimeTransformObj.AddComponent<MockRuntimeTransformHandle>();
        transformObjectsManager.runtimeTransformObj = runtimeTransformObj;
        mockMediator = new MockMediator();
        transformObjectsManager.mediator = mockMediator;
        transformableObject0 = new GameObject("transformableObject0");
        transformableObject0.tag = TagsEnum.transformableObject;
        transformableObject1 = new GameObject("transformableObject1");
        transformableObject1.tag = TagsEnum.transformableObject;
        nonTransformableObject = new GameObject("nonTransformableObject");
        nonTransformableObject.tag = TagsEnum.selectableObject;
    }

    [UnityTest]
    public IEnumerator ShouldActivateAndDeactivateTranslationMode()
    {
        Assert.AreEqual(false, mockMediator.transformModeActive);
        yield return null;
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = transformWrapper,
            wrappedObjects = new List<GameObject>() { }
        };
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.IsTrue(mockMediator.transformModeActive);
        yield return null;
        transformObjectsManager.DeactivateTransformMode();
        Assert.IsFalse(mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldActivateAndDeactivateRotationMode()
    {
        Assert.AreEqual(false, mockMediator.transformModeActive);
        yield return null;
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = transformWrapper,
            wrappedObjects = new List<GameObject>() { }
        };
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformObjectsManager.DeactivateTransformMode();
        Assert.AreEqual(false, mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldSwitchFromTranslationToRotationModeAndBackwards()
    {
        Assert.AreEqual(false, mockMediator.transformModeActive);
        yield return null;
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = transformWrapper,
            wrappedObjects = new List<GameObject>() { }
        };
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldRaiseExceptionIfAttemptedToDeactivateTransformModeButNoModeIsActiveToBeginWith()
    {
        Assert.AreEqual(false, mockMediator.notified);
        yield return null;
        Assert.That(() => transformObjectsManager.DeactivateTransformMode(),
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
        Assert.That(() => transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => transformObjectsManager.ActivateTransformMode(null, TransformMode.PositionMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => transformObjectsManager.ActivateTransformMode(null, TransformMode.RotationMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldAllowToActivateTransformModeWhenSelectedOnlyTransformableObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { transformableObject0, transformableObject1},
        };
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldNotActivatePositionModeIfAttemptedToTransformNotTransformableObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { transformableObject0, transformableObject1, nonTransformableObject }
        };
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.IsFalse(transformObjectsManager.active);
        Assert.IsFalse(mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldNotActivateRotationModeIfAttemptedToTransformNotTransformableObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { transformableObject0, transformableObject1, nonTransformableObject }
        };
        transformObjectsManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        yield return null;
        Debug.Log("Asserting");
        Assert.IsFalse(transformObjectsManager.active);
        Assert.IsFalse(mockMediator.transformModeActive);
        yield return null;
    }

    private class MockMediator : IMediator
    {
        public TransformMode mode;
        public bool transformModeActive = false;
        public bool notified = false;
        public void Notify(Events eventName)
        {
            switch (eventName)
            {
                case Events.positionTransformModeActivated:
                    transformModeActive = true;
                    mode = TransformMode.PositionMode;
                    notified = true;
                    break;
                case Events.rotationTransformModeActivated:
                    transformModeActive = true;
                    mode = TransformMode.RotationMode;
                    notified = true;
                    break;
                case Events.transformModeDeactivated:
                    transformModeActive = false;
                    notified = true;
                    break;
            }
        }

        public void Notify<T>(Events eventName, T payload)
        {
            throw new System.NotImplementedException();
        }
    }
    private class MockRuntimeTransformHandle : MonoBehaviour, IRuntimeTransformHandle
    {
        public Transform target { set { } }
        public IMediator mediator { set { } }
        public bool autoScale { set { } }
        public float autoScaleFactor { set { } }
        public TransformHandleType type { set { } }
    }
}