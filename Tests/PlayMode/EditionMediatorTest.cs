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
    }

    [UnityTest]
    public IEnumerator ShouldSendMessageInSetEditModeToTrue()
    {
        editionMediator.Notify(Events.setEditMode, true);
        WebMessage<bool> sentMessage = (WebMessage<bool>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual(true, sentMessage.payload);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldSendMessageInSetEditModeToFalse()
    {
        editionMediator.Notify(Events.setEditMode, false);
        WebMessage<bool> sentMessage = (WebMessage<bool>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual(false, sentMessage.payload);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldSetEditModeWhenReceiveRequest()
    {
        string message = dummyJsonCreator.createWebMessage(WebOperationsEnum.setEditMode, "true");
        editionMediator.ReceiveWebMessage(message);
        Assert.AreEqual(mockEditModeManager.editMode, true);
        yield return null;
        message = dummyJsonCreator.createWebMessage(WebOperationsEnum.setEditMode, "false");
        editionMediator.ReceiveWebMessage(message);
        Assert.AreEqual(mockEditModeManager.editMode, false);
        yield return null;
    }
    [UnityTest]
    public IEnumerator EditionMediatorShouldAllowAndDisallowObjectSelection()
    {
        editionMediator.Notify(Events.setEditMode, true);
        Assert.AreEqual(mockSelectedObjectsManager.allowSelection, true);
        yield return null;
        editionMediator.Notify(Events.setEditMode, false);
        Assert.AreEqual(mockSelectedObjectsManager.allowSelection, false);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldClearSelectionWhenEditModeIsSetToFalse()
    {
        editionMediator.Notify(Events.setEditMode, false);
        Assert.AreEqual(mockSelectedObjectsManager.selectionCleared, true);
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

        public GameObject selection => throw new System.NotImplementedException();

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
    private static class dummyJsonCreator
    {
        public static string createWebMessage(string type, string payload)
        {
            return $"{{\"type\":\"{type}\",\"payload\":{payload}}}";
        }
    }

}
