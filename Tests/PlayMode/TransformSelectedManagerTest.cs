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

public class TransformSelectedManagerTest : MonoBehaviour
{
    GameObject containerGameObject;
    GameObject runtimeTransformObj;
    GameObject transformWrapper;
    TransformSelectedManager transformSelectedManager;
    MockMediator mockMediator;
    GameObject transformableObject0;
    GameObject transformableObject1;
    GameObject nonTransformableObject;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject("containerGameObject");
        transformWrapper = new GameObject("transformWrapper");
        transformSelectedManager = containerGameObject.AddComponent<TransformSelectedManager>();
        runtimeTransformObj = new GameObject("TransformHandle");
        runtimeTransformObj.AddComponent<MockRuntimeTransformHandle>();
        transformSelectedManager.runtimeTransformObj = runtimeTransformObj;
        mockMediator = new MockMediator();
        transformSelectedManager.mediator = mockMediator;
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
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.DeactivateTransformMode();
        Assert.AreEqual(false, mockMediator.transformModeActive);
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
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.DeactivateTransformMode();
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
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldRaiseExceptionIfAttemptedToDeactivateTransformModeButNoModeIsActiveToBeginWith()
    {
        Assert.AreEqual(false, mockMediator.notified);
        yield return null;
        Assert.That(() => transformSelectedManager.DeactivateTransformMode(),
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
        Assert.That(() => transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => transformSelectedManager.ActivateTransformMode(null, TransformMode.PositionMode),
            Throws.TypeOf<ArgumentException>()
        );
        yield return null;
        Assert.That(() => transformSelectedManager.ActivateTransformMode(null, TransformMode.RotationMode),
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
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldRaiseExceptionIfAttemptedToTransformNotTransformableObject()
    {
        ObjectWrapperDTO objectWrapperDTO = new ObjectWrapperDTO()
        {
            wrapper = new GameObject("wrapper"),
            wrappedObjects = new List<GameObject>() { transformableObject0, transformableObject1, nonTransformableObject }
        };
        Assert.That(() => transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.PositionMode),
            Throws.TypeOf<InvalidOperationException>()
        );
        Assert.That(() => transformSelectedManager.ActivateTransformMode(objectWrapperDTO, TransformMode.RotationMode),
            Throws.TypeOf<InvalidOperationException>()
        );
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
