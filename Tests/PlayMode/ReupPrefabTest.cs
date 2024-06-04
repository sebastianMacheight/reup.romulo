using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

using ReupVirtualTwin.managers;
using ReupVirtualTwin.models;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helpers;

public class ReupPrefabTest : MonoBehaviour
{
    GameObject reupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.reup.romulo/Assets/Quickstart/Reup.prefab");
    GameObject reupGameObject;
    GameObject building;
    GameObject character;

    IObjectRegistry objectRegistry;
    IBuildingGetterSetter setupBuilding;

    EditionMediator editionMediator;
    SensedObjectHighlighter selectableObjectHighlighter;
    EditModeManager editModeManager;


    [UnitySetUp]
    public IEnumerator SetUp()
    {
        CreateComponents();
        CreateBuilding();
        yield return null;
    }

    private void CreateComponents()
    {
        reupGameObject = (GameObject)PrefabUtility.InstantiatePrefab(reupPrefab);

        GameObject baseGlobalScriptGameObject = reupGameObject.transform.Find("BaseGlobalScripts").gameObject;
        objectRegistry = baseGlobalScriptGameObject.transform.Find("ObjectRegistry").GetComponent<IObjectRegistry>();
        setupBuilding = baseGlobalScriptGameObject.transform.Find("SetupBuilding").GetComponent<IBuildingGetterSetter>();

        GameObject editionMediatorGameObject = baseGlobalScriptGameObject.transform.Find("EditionMediator").gameObject;
        editionMediator = editionMediatorGameObject.GetComponent<EditionMediator>();
        editModeManager = editionMediatorGameObject.transform.Find("EditModeManager").GetComponent<EditModeManager>();

        character = reupGameObject.transform.Find("Character").gameObject;
        selectableObjectHighlighter = character.transform
            .Find("Behaviours")
            .Find("HoverOverSelectablesObjects")
            .gameObject.GetComponent<SensedObjectHighlighter>();
    }

    private void CreateBuilding()
    {
        building = new GameObject("building");
        building.AddComponent<RegisteredIdentifier>().AssignId("building-id");
        setupBuilding.building = building;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Destroy(reupGameObject);
        Destroy(building);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldFindTheRegistry()
    {
        Assert.AreEqual(objectRegistry, editionMediator.registry);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditionMediatorShouldHaveAChangeMaterialController()
    {
        Assert.IsNotNull(editionMediator.changeMaterialController);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectableObjectHighlighter_should_have_objectSensor()
    {
        Assert.IsNotNull(selectableObjectHighlighter.objectSensor);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectableObjectHighlighter_should_have_objectHighlighter()
    {
        Assert.IsNotNull(selectableObjectHighlighter.objectHighlighter);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectableObjectHighlighter_should_have_selectedObjectsManager()
    {
        Assert.IsNotNull(selectableObjectHighlighter.selectedObjectsManager);
        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectableObjectHighlighterObjectSensor_should_haveSelectableObjectSelector()
    {
        ObjectSensor selectableObjectSensorHighligherObjectSensor = (ObjectSensor) selectableObjectHighlighter.objectSensor;
        Assert.AreEqual(typeof(SelectableObjectSelector), selectableObjectSensorHighligherObjectSensor.objectSelector.GetType());
        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectableObjectsHighlighter_should_beEnabled_onlyWhen_EditModeIsEnabled()
    {
        Assert.IsFalse(selectableObjectHighlighter.enableHighlighting);

        editModeManager.editMode = true;
        yield return null;
        Assert.IsTrue(selectableObjectHighlighter.enableHighlighting);
        editModeManager.editMode = false;
        yield return null;
        Assert.IsFalse(selectableObjectHighlighter.enableHighlighting);
        yield return null;
    }

}
