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
using ReupVirtualTwinTests.behaviours;


public class ReupPrefabTest : MonoBehaviour
{
    GameObject reupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.reup.romulo/Assets/Quickstart/Reup.prefab");
    GameObject reupGameObject;
    GameObject building;

    IObjectRegistry objectRegistry;
    EditionMediator editionMediator;
    IBuildingGetterSetter setupBuilding;
    SensedObjectHighlighter selectableObjectHighlighter;

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
        editionMediator = baseGlobalScriptGameObject.transform.Find("EditionMediator").GetComponent<EditionMediator>();
        setupBuilding = baseGlobalScriptGameObject.transform.Find("SetupBuilding").GetComponent<IBuildingGetterSetter>();

        GameObject character = reupGameObject.transform.Find("Character").gameObject;

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

}
