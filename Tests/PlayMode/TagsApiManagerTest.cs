using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using ReupVirtualTwin.controllers;
using System.Threading.Tasks;
using Packages.reup.romulo.Tests.PlayMode.Mocks;
using ReupVirtualTwin.dataModels;

public class TagsApiManagerTest : MonoBehaviour
{
    GameObject tagsApiManagerGameObject;
    TagsApiManager tagsApiManager;
    MockTagsWebRequester mockTagsWebRequester;

    [SetUp]
    public void SetUp()
    {
        tagsApiManagerGameObject = new GameObject("TagApiManagerGameObject");
        tagsApiManager = tagsApiManagerGameObject.AddComponent<TagsApiManager>();
        mockTagsWebRequester = new MockTagsWebRequester();
        tagsApiManager.webRequester = mockTagsWebRequester;
    }

    [TearDown]
    public void TearDown()
    {
        Destroy(tagsApiManagerGameObject);
    }

    [Test]
    public async Task GetInitialLoadOfTags()
    {
        List<ObjectTag> initialTags = await tagsApiManager.GetTags();
        Assert.AreEqual(3, initialTags.Count);
        Assert.AreEqual("tag0", initialTags[0].name);
        Assert.AreEqual("tag1", initialTags[1].name);
        Assert.AreEqual("tag2", initialTags[2].name);
        Assert.AreEqual(1, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(1, mockTagsWebRequester.timesFetched);
    }

    [Test]
    public async Task ShouldGetAlreadyFetchedTagsWithoutFetchingMore()
    {
        await tagsApiManager.GetTags();
        Assert.AreEqual(1, mockTagsWebRequester.timesFetched);
        Assert.AreEqual(1, mockTagsWebRequester.lastPageRequested);
        List<ObjectTag> initialTags = await tagsApiManager.GetTags();
        Assert.AreEqual(3, initialTags.Count);
        Assert.AreEqual("tag0", initialTags[0].name);
        Assert.AreEqual("tag1", initialTags[1].name);
        Assert.AreEqual("tag2", initialTags[2].name);
        Assert.AreEqual(1, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(1, mockTagsWebRequester.timesFetched);
    }

    [Test]
    public async Task GetSecondLoadOfTags()
    {
        List<ObjectTag> initialTags = await tagsApiManager.GetTags();
        Assert.AreEqual(3, initialTags.Count);
        Assert.AreEqual("tag0", initialTags[0].name);
        Assert.AreEqual("tag1", initialTags[1].name);
        Assert.AreEqual("tag2", initialTags[2].name);
        Assert.AreEqual(1, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(1, mockTagsWebRequester.timesFetched);
        List<ObjectTag> moreTags = await tagsApiManager.LoadMoreTags();
        Assert.AreEqual(6, moreTags.Count);
        Assert.AreEqual("tag0", moreTags[0].name);
        Assert.AreEqual("tag1", moreTags[1].name);
        Assert.AreEqual("tag2", moreTags[2].name);
        Assert.AreEqual("tag3", moreTags[3].name);
        Assert.AreEqual("tag4", moreTags[4].name);
        Assert.AreEqual("tag5", moreTags[5].name);
        Assert.AreEqual(2, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(2, mockTagsWebRequester.timesFetched);
    }

    [Test]
    public async Task GetThirdLoadOfTags()
    {
        await tagsApiManager.GetTags();
        Assert.AreEqual(1, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(1, mockTagsWebRequester.timesFetched);
        await tagsApiManager.LoadMoreTags();
        Assert.AreEqual(2, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(2, mockTagsWebRequester.timesFetched);
        List<ObjectTag> moreTags = await tagsApiManager.LoadMoreTags();
        Assert.AreEqual("tag0", moreTags[0].name);
        Assert.AreEqual("tag1", moreTags[1].name);
        Assert.AreEqual("tag2", moreTags[2].name);
        Assert.AreEqual("tag3", moreTags[3].name);
        Assert.AreEqual("tag4", moreTags[4].name);
        Assert.AreEqual("tag5", moreTags[5].name);
        Assert.AreEqual("tag6", moreTags[6].name);
        Assert.AreEqual("tag7", moreTags[7].name);
        Assert.AreEqual(8, moreTags.Count);
        Assert.AreEqual(3, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(3, mockTagsWebRequester.timesFetched);
    }
    [Test]
    public async Task ShouldNotFetchApiIfNoMoreTagsAreAvailable()
    {
        await tagsApiManager.LoadMoreTags();
        await tagsApiManager.LoadMoreTags();
        await tagsApiManager.LoadMoreTags();
        Assert.AreEqual(3, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(3, mockTagsWebRequester.timesFetched);
        await tagsApiManager.LoadMoreTags();
        await tagsApiManager.LoadMoreTags();
        Assert.AreEqual(3, mockTagsWebRequester.lastPageRequested);
        Assert.AreEqual(3, mockTagsWebRequester.timesFetched);
    }

}

