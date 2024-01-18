using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using ReupVirtualTwin.models;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.behaviours;

public class TagsHandlerTest : MonoBehaviour
{
    GameObject tagsHandlerContainer;
    GameObject taggedObject0;
    GameObject taggedObject1;
    GameObject nonTaggedObject0;
    TagsHandler tagsHandler;
    ObjectTags objectTags0;
    ObjectTags objectTags1;

    [SetUp]
    public void SetUp()
    {
        tagsHandlerContainer = new GameObject("tagsHandlerContainer");
        tagsHandler = tagsHandlerContainer.AddComponent<TagsHandler>();
        taggedObject0 = new GameObject("taggedObj0");
        objectTags0 = taggedObject0.AddComponent<ObjectTags>();
        objectTags0.AddTags(new ObjectTag[2] {ObjectTag.SELECTABLE, ObjectTag.TRANSFORMABLE});
        taggedObject1 = new GameObject("taggedObject1");
        objectTags1 = taggedObject1.AddComponent<ObjectTags>();
        objectTags1.AddTag(ObjectTag.SELECTABLE);
        nonTaggedObject0 = new GameObject("nonTaggedObj0");
    }
    [TearDown]
    public void TearDown()
    {
        Destroy(taggedObject0);
    }
    [UnityTest]
    public IEnumerator ShouldReturnFalseOnCheckForTags()
    {
        Assert.IsFalse(tagsHandler.DoesObjectHaveTag(taggedObject0, ObjectTag.DELETABLE));
        Assert.IsFalse(tagsHandler.DoesObjectHaveTag(taggedObject1, ObjectTag.DELETABLE));
        Assert.IsFalse(tagsHandler.DoesObjectHaveTag(nonTaggedObject0, ObjectTag.DELETABLE));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldReturnTrueOnCheckForTags()
    {
        Assert.IsTrue(tagsHandler.DoesObjectHaveTag(taggedObject0, ObjectTag.SELECTABLE));
        Assert.IsTrue(tagsHandler.DoesObjectHaveTag(taggedObject1, ObjectTag.SELECTABLE));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldAddTags()
    {
        Assert.IsFalse(tagsHandler.DoesObjectHaveTag(taggedObject0, ObjectTag.DELETABLE));
        tagsHandler.AddTagToObject(taggedObject0, ObjectTag.DELETABLE);
        yield return null;
        Assert.IsTrue(tagsHandler.DoesObjectHaveTag(taggedObject0, ObjectTag.DELETABLE));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldRemoveTags()
    {
        Assert.IsTrue(tagsHandler.DoesObjectHaveTag(taggedObject0, ObjectTag.SELECTABLE));
        tagsHandler.RemoveTagFromOjbect(taggedObject0, ObjectTag.SELECTABLE);
        yield return null;
        Assert.IsFalse(tagsHandler.DoesObjectHaveTag(taggedObject0, ObjectTag.SELECTABLE));
        yield return null;
    }

}
