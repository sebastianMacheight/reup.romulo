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
    MockMediator mockMediator;

    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject();
        editModeManager = containerGameObject.AddComponent<EditModeManager>();
        mockMediator = new MockMediator();
        editModeManager.mediator = mockMediator;
    }

    [UnityTest]
    public IEnumerator EditModeManagerShouldNotifyMediatorWhenSettingEditMode()
    {
        editModeManager.editMode = true;
        Assert.AreEqual(true, mockMediator.editMode);
        yield return null;
        editModeManager.editMode = false;
        Assert.AreEqual(false, mockMediator.editMode);
        yield return null;
    }

    private class MockMediator : IMediator
    {
        public bool editMode;
        public void Notify(Events eventName)
        {
            throw new System.NotImplementedException();
        }

        public void Notify<T>(Events eventName, T payload)
        {
            if (eventName == Events.setEditMode)
            {
                editMode = (bool)(object)payload;
            }
        }
    }

}
