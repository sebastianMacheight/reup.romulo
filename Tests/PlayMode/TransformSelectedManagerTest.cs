using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;

public class TransformSelectedManagerTest : MonoBehaviour
{
    GameObject containerGameObject;
    GameObject transformWrapper;
    TransformSelectedManager transformSelectedManager;
    MockMediator mockMediator;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject("containerGameObject");
        transformWrapper = new GameObject("transformWrapper");
        transformSelectedManager = containerGameObject.AddComponent<TransformSelectedManager>();
        mockMediator = new MockMediator();
        transformSelectedManager.mediator = mockMediator;
    }

    [UnityTest]
    public IEnumerator ShouldActivateAndDeactivateTranslationMode()
    {
        Assert.AreEqual(false, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.ActivateTransformMode(transformWrapper, TransformMode.PositionMode);
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
        transformSelectedManager.ActivateTransformMode(transformWrapper, TransformMode.RotationMode);
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
        transformSelectedManager.ActivateTransformMode(transformWrapper, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.ActivateTransformMode(transformWrapper, TransformMode.RotationMode);
        Assert.AreEqual(TransformMode.RotationMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
        transformSelectedManager.ActivateTransformMode(transformWrapper, TransformMode.PositionMode);
        Assert.AreEqual(TransformMode.PositionMode, mockMediator.mode);
        Assert.AreEqual(true, mockMediator.transformModeActive);
        yield return null;
    }

    private class MockMediator : IMediator
    {
        public TransformMode mode;
        public bool transformModeActive = false;
        public void Notify(Events eventName)
        {
            switch (eventName)
            {
                case Events.positionTransformModeActivated:
                    transformModeActive = true;
                    mode = TransformMode.PositionMode;
                    break;
                case Events.rotationTransformModeActivated:
                    transformModeActive = true;
                    mode = TransformMode.RotationMode;
                    break;
                case Events.transformModeDeactivated:
                    transformModeActive = false;
                    break;
            }
        }

        public void Notify<T>(Events eventName, T payload)
        {
            throw new System.NotImplementedException();
        }
    }
}
