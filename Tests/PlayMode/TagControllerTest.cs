using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using ReupVirtualTwin.models;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.controllers;

public class tagsControllerTest : MonoBehaviour
{
    GameObject taggedObject0;
    GameObject taggedObject1;
    GameObject nonTaggedObject0;
    TagsController tagsController;
    ObjectTags objectTags0;
    ObjectTags objectTags1;

    [SetUp]
    public void SetUp()
    {
        tagsController = new TagsController();
        taggedObject0 = new GameObject("taggedObj0");
        objectTags0 = taggedObject0.AddComponent<ObjectTags>();
        objectTags0.AddTags(new string[2] {ObjectTag.SELECTABLE.ToString(), ObjectTag.TRANSFORMABLE.ToString() });
        taggedObject1 = new GameObject("taggedObject1");
        objectTags1 = taggedObject1.AddComponent<ObjectTags>();
        objectTags1.AddTag(ObjectTag.SELECTABLE.ToString());
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
        Assert.IsFalse(tagsController.DoesObjectHaveTag(taggedObject0, ObjectTag.DELETABLE.ToString()));
        Assert.IsFalse(tagsController.DoesObjectHaveTag(taggedObject1, ObjectTag.DELETABLE.ToString()));
        Assert.IsFalse(tagsController.DoesObjectHaveTag(nonTaggedObject0, ObjectTag.DELETABLE.ToString()));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldReturnTrueOnCheckForTags()
    {
        Assert.IsTrue(tagsController.DoesObjectHaveTag(taggedObject0, ObjectTag.SELECTABLE.ToString()));
        Assert.IsTrue(tagsController.DoesObjectHaveTag(taggedObject1, ObjectTag.SELECTABLE.ToString()));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldAddTags()
    {
        Assert.IsFalse(tagsController.DoesObjectHaveTag(taggedObject0, ObjectTag.DELETABLE.ToString()));
        tagsController.AddTagToObject(taggedObject0, ObjectTag.DELETABLE.ToString());
        yield return null;
        Assert.IsTrue(tagsController.DoesObjectHaveTag(taggedObject0, ObjectTag.DELETABLE.ToString()));
        yield return null;
    }
    [UnityTest]
    public IEnumerator ShouldRemoveTags()
    {
        Assert.IsTrue(tagsController.DoesObjectHaveTag(taggedObject0, ObjectTag.SELECTABLE.ToString()));
        tagsController.RemoveTagFromOjbect(taggedObject0, ObjectTag.SELECTABLE.ToString());
        yield return null;
        Assert.IsFalse(tagsController.DoesObjectHaveTag(taggedObject0, ObjectTag.SELECTABLE.ToString()));
        yield return null;
    }

}
