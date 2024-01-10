using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;

public class EditionMediatorTest : MonoBehaviour
{
    EditionMediator editionMediator;
    EditModeManager editModeManager;
    MockWebMessageSender mockWebMessageSender;
    GameObject containerGameObject;
    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject();
        editionMediator = containerGameObject.AddComponent<EditionMediator>();
        editModeManager = containerGameObject.AddComponent<EditModeManager>();
        editionMediator.editModeManager = editModeManager;
        editModeManager.mediator = editionMediator;
        mockWebMessageSender = new MockWebMessageSender();
        editModeManager.webMessageSender = mockWebMessageSender;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldSendWebMessageOnEditModeSwitch()
    {
        editionMediator.ReceiveSetEditModeRequest("true");
        WebMessage<string> sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual("true", sentMessage.payload);
        yield return null;
        mockWebMessageSender.sentMessage = null;
        yield return null;
        editionMediator.ReceiveSetEditModeRequest("false");
        sentMessage = (WebMessage<string>)mockWebMessageSender.sentMessage;
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, sentMessage.type);
        Assert.AreEqual("false", sentMessage.payload);
        yield return null;
    }
}
public class MockWebMessageSender : IWebMessagesSender
{
    public object sentMessage;
    public void SendWebMessage<T>(WebMessage<T> webWessage)
    {
        sentMessage = webWessage;
    }
}
