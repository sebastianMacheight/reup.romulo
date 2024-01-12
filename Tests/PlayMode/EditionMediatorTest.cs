using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using ReupVirtualTwin.managers;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;

public class EditionMediatorTest : MonoBehaviour
{
    GameObject containerGameObject;
    EditionMediator editionMediator;
    MockEditModeManager mockEditModeManager;
    MockSelectedObjectsManager mockSelectedObjectsManager;
    MockWebMessageSender mockWebMessageSender;
    MockTransformSelectedManager mockTransformSelectedManager;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject();
        editionMediator = containerGameObject.AddComponent<EditionMediator>();
        mockEditModeManager = new MockEditModeManager();
        mockSelectedObjectsManager = new MockSelectedObjectsManager();
        editionMediator.editModeManager = mockEditModeManager;
        editionMediator.selectedObjectsManager = mockSelectedObjectsManager;
        mockWebMessageSender = new MockWebMessageSender();
        editionMediator.webMessageSender = mockWebMessageSender;
        mockTransformSelectedManager = new MockTransformSelectedManager();
        editionMediator.transformSelectedManager = mockTransformSelectedManager;
    }

    [UnityTest]
    public IEnumerator ShouldSendMessageInSetEditModeToTrue()
    {
        editionMediator.Notify(Events.setEditMode, true);
        WebMessage<bool> sentMessage = (WebMessage<bool>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebMessageType.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual(true, sentMessage.payload);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldSendMessageInSetEditModeToFalse()
    {
        editionMediator.Notify(Events.setEditMode, false);
        WebMessage<bool> sentMessage = (WebMessage<bool>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebMessageType.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual(false, sentMessage.payload);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldSetEditModeWhenReceiveRequest()
    {
        string message = dummyJsonCreator.createWebMessage(WebMessageType.setEditMode, "true");
        editionMediator.ReceiveWebMessage(message);
        Assert.AreEqual(mockEditModeManager.editMode, true);
        yield return null;
        message = dummyJsonCreator.createWebMessage(WebMessageType.setEditMode, "false");
        editionMediator.ReceiveWebMessage(message);
        Assert.AreEqual(mockEditModeManager.editMode, false);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldAllowAndDisallowObjectSelection()
    {
        editionMediator.Notify(Events.setEditMode, true);
        Assert.AreEqual(mockSelectedObjectsManager.allowSelection, true);
        yield return null;
        editionMediator.Notify(Events.setEditMode, false);
        Assert.AreEqual(mockSelectedObjectsManager.allowSelection, false);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldClearSelectionWhenEditModeIsSetToFalse()
    {
        editionMediator.Notify(Events.setEditMode, false);
        Assert.AreEqual(mockSelectedObjectsManager.selectionCleared, true);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShouldSendErrorMessageIfReceibeMessageToActivatePositionTransformModeWithNoSelectedObject()
    {
        string message = dummyJsonCreator.createWebMessage(WebMessageType.activatePositionTransform);
        editionMediator.ReceiveWebMessage(message);
        yield return null;
        WebMessage<string> sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebMessageType.error, sentMessage.type);
        Assert.AreEqual($"Can't activate {TransformMode.PositionMode} Transform mode because nothing is selected", sentMessage.payload);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldSendErrorMessageIfReceibeMessageToActivateRotationTransformModeWithNoSelectedObject()
    {
        string message = dummyJsonCreator.createWebMessage(WebMessageType.activateRotationTransform);
        editionMediator.ReceiveWebMessage(message);
        yield return null;
        WebMessage<string> sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebMessageType.error, sentMessage.type);
        Assert.AreEqual($"Can't activate {TransformMode.RotationMode} Transform mode because nothing is selected", sentMessage.payload);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldSendErrorMessageIfReceibeMessageToDeactivateTransformModeButNoTransforModeIsActive()
    {
        string message = dummyJsonCreator.createWebMessage(WebMessageType.deactivateTransformMode);
        editionMediator.ReceiveWebMessage(message);
        yield return null;
        WebMessage<string> sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebMessageType.error, sentMessage.type);
        Assert.AreEqual("Can't deactivate transform mode if no transform mode is currently active", sentMessage.payload);
        yield return null;
    }

    private class MockEditModeManager : IEditModeManager
    {
        private bool _editMode;
        public bool editMode { get => _editMode; set => _editMode = value; }
    }
    private class MockSelectedObjectsManager: ISelectedObjectsManager
    {
        private bool _allowSelection = false;
        public bool allowSelection { get => _allowSelection; set => _allowSelection = value; }
        public bool selectionCleared = false;

        private GameObject _selection = null;

        public GameObject selection => _selection;

        public GameObject AddObjectToSelection(GameObject selectedObject)
        {
            throw new System.NotImplementedException();
        }

        public void ClearSelection()
        {
            selectionCleared = true;
        }

        public GameObject RemoveObjectFromSelection(GameObject selectedObject)
        {
            throw new System.NotImplementedException();
        }
    }

    private class MockWebMessageSender : IWebMessagesSender
    {
        public object sentMessage;
        public void SendWebMessage<T>(WebMessage<T> webWessage)
        {
            sentMessage = webWessage;
        }
    }
    private class MockTransformSelectedManager : ITransformSelectedManager
    {
        public bool _active = false;
        public GameObject wrapper
        {
            set
            {

            }
        }
        public bool active => _active;

        public void ActivateTransformMode(GameObject wrapper, TransformMode mode)
        {
            _active = true;
        }

        public void DeactivateTransformMode()
        {
            _active = false;
        }
    }
    private static class dummyJsonCreator
    {
        public static string createWebMessage(string type)
        {
            return $"{{\"type\":\"{type}\"}}";
        }
        public static string createWebMessage(string type, string payload)
        {
            return $"{{\"type\":\"{type}\",\"payload\":\"{payload}\"}}";
        }
    }

}
