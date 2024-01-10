using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.managers;
using UnityEngine.TestTools;
using NUnit.Framework;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.enums;

public class EditModeManagerTest : MonoBehaviour
{
    GameObject containerGameObject;
    EditModeManager editModeManager;
    MockWebMessageSender mockWebMessageSender;
    MockMediator mockMediator;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject();
        editModeManager = containerGameObject.AddComponent<EditModeManager>();
        mockMediator = new MockMediator();
        editModeManager.mediator = mockMediator;
        mockWebMessageSender = new MockWebMessageSender();
        editModeManager.webMessageSender = mockWebMessageSender;
    }

    [UnityTest]
    public IEnumerator EditModeManagerShouldSendMessageInSetEditModeToTrue()
    {
        editModeManager.editMode = true;
        WebMessage<string> sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual("true", sentMessage.payload);
        yield return null;
    }
    [UnityTest]
    public IEnumerator EditModeManagerShouldSendMessageInSetEditModeToFalse()
    {
        editModeManager.editMode = false;
        WebMessage<string> sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual("false", sentMessage.payload);
        yield return null;
    }

    private class MockWebMessageSender : IWebMessagesSender
    {
        public object sentMessage;
        public void SendWebMessage<T>(WebMessage<T> webWessage)
        {
            sentMessage = webWessage;
        }
    }

    private class MockMediator : IMediator
    {
        public string editMode = null;
        public void Notify(Events eventName)
        {
            throw new System.NotImplementedException();
        }

        public void Notify(Events eventName, string payload)
        {
            if (eventName == Events.setEditMode)
            {
                editMode = payload;
            }
        }
    }

}
