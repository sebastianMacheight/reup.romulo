using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.test
{
    public class EditModeManagerTest : MonoBehaviour
    {
        EditModeManager editModeManager;
        MockWebMessageSender mockWebMessageSender;
        GameObject editModeManagerObject;
        [SetUp]
        public void SetUp()
        {
            editModeManagerObject = new GameObject();
            editModeManager = editModeManagerObject.AddComponent<EditModeManager>();
            mockWebMessageSender = new MockWebMessageSender();
            editModeManager.webMessageSender = mockWebMessageSender;
        }

        [UnityTest]
        public IEnumerator EditModeManagerShouldSendWebMessageOnModeSwitch()
        {
            editModeManager.ReceiveSetEditModeRequest(true);
            Assert.AreEqual(WebOperationsEnum.setEditMode, mockWebMessageSender.sentMessage.operation);
            Assert.AreEqual("true", mockWebMessageSender.sentMessage.body);
            yield return null;
            mockWebMessageSender.sentMessage = null;
            yield return null;
            editModeManager.ReceiveSetEditModeRequest(false);
            Assert.AreEqual(WebOperationsEnum.setEditMode, mockWebMessageSender.sentMessage.operation);
            Assert.AreEqual("false", mockWebMessageSender.sentMessage.body);
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
}
