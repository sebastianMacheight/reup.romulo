using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using ReupVirtualTwin.managers;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.enums;

public class EditionMediatorTest : MonoBehaviour
{
    GameObject containerGameObject;
    EditionMediator editionMediator;
    MockEditModeManager mockEditModeManager;
    MockSelectedObjectsManager mockSelectedObjectsManager;
    [SetUp]
    public void SetUp()
    {
        containerGameObject = new GameObject();
        editionMediator = containerGameObject.AddComponent<EditionMediator>();
        mockEditModeManager = new MockEditModeManager();
        mockSelectedObjectsManager = new MockSelectedObjectsManager();
        editionMediator.editModeManager = mockEditModeManager;
        editionMediator.selectedObjectsManager = mockSelectedObjectsManager;
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
    [UnityTest]
    public IEnumerator EditionMediatorShouldAllowAndDisallowObjectSelection()
    {
        editionMediator.Notify(Events.setEditMode, "true");
        Assert.AreEqual(mockSelectedObjectsManager.allowSelection, true);
        yield return null;
        editionMediator.Notify(Events.setEditMode, "false");
        Assert.AreEqual(mockSelectedObjectsManager.allowSelection, false);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldClearSelectionWhenEditModeIsSetToFalse()
    {
        editionMediator.Notify(Events.setEditMode, "false");
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
}
