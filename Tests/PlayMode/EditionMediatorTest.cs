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
        mockWebMessageSender = new MockWebMessageSender();
        editModeManager.webMessageSender = mockWebMessageSender;
    }

    [UnityTest]
    public IEnumerator EditModeManagerShouldSendWebMessageOnModeSwitch()
    {
        editionMediator.ReceiveSetEditModeRequest("true");
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, mockWebMessageSender.sentMessage.type);
        Assert.AreEqual("true", mockWebMessageSender.sentMessage.payload);
        yield return null;
        mockWebMessageSender.sentMessage = null;
        yield return null;
        editionMediator.ReceiveSetEditModeRequest("false");
        Assert.AreEqual(WebOperationsEnum.setEditModeSuccess, mockWebMessageSender.sentMessage.type);
        Assert.AreEqual("false", mockWebMessageSender.sentMessage.payload);
        yield return null;
    }
}
public class MockWebMessageSender : IWebMessagesSender
{
    public WebMessage sentMessage;
    public void SendWebMessage(WebMessage webWessage)
    {
        sentMessage = webWessage;
    }
}
