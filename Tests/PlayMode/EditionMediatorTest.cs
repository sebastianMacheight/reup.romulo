using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using ReupVirtualTwin.managers;
using ReupVirtualTwin.managerInterfaces;

public class EditionMediatorTest : MonoBehaviour
{
    GameObject containerGameObject;
    EditionMediator editionMediator;
    MockEditModeManager mockEditModeManager;
    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject();
        editionMediator = containerGameObject.AddComponent<EditionMediator>();
        mockEditModeManager = new MockEditModeManager();
        editionMediator.editModeManager = mockEditModeManager;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldSetEditModeWhenReceiveRequest()
    {
        editionMediator.ReceiveSetEditModeRequest("true");
        Assert.AreEqual(mockEditModeManager.editMode, true);
        yield return null;
        editionMediator.ReceiveSetEditModeRequest("false");
        Assert.AreEqual(mockEditModeManager.editMode, false);
        yield return null;
    }

    private class MockEditModeManager : IEditModeManager
    {
        private bool _editMode;
        public bool editMode { get => _editMode; set => _editMode = value; }
    }
}
