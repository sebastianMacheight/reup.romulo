using UnityEngine;
using NUnit.Framework;

using ReupVirtualTwin.models;
using ReupVirtualTwin.controllers;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.controllerInterfaces;
using System.Collections.Generic;

namespace ReupVirtualTwinTests.controllers
{
    public class TagFilterApplierTest : MonoBehaviour
    {
        GameObject building;

        [SetUp]
        public void SetUp()
        {
            building = StubObjectTreeCreator.CreateMockBuilding();
        }
        [TearDown]
        public void TearDown()
        {
            Destroy(building);
        }
        [Test]
        public void ShouldGetParent()
        {
            ITagFilter filterparent = new TagFilter(StubObjectTreeCreator.parentTags[0]);
            List<ITagFilter> filterList = new List<ITagFilter>() { filterparent };
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFilters(building, filterList);
            Assert.AreEqual(1, filteredObjects.Count);
            Assert.AreEqual(building, filteredObjects[0]);
        }
        [Test]
        public void ShouldGetEmptyGameObjectList()
        {
            ITagFilter filterparent = new TagFilter(StubObjectTreeCreator.parentTags[0]);
            ITagFilter filterParentInverse = new TagFilter(StubObjectTreeCreator.parentTags[0]);
            filterParentInverse.invertFilter = true;
            List<ITagFilter> filterList = new List<ITagFilter>() { filterparent, filterParentInverse };
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFilters(building, filterList);
            Assert.AreEqual(0, filteredObjects.Count);
        }

        [Test]
        public void ShouldGetChildrenObjects()
        {
            List<ITagFilter> filterList = new List<ITagFilter>() {new TagFilter(StubObjectTreeCreator.commonChildrenTag)};
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFilters(building, filterList);
            Assert.AreEqual(2, filteredObjects.Count);
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(0).gameObject));
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(1).gameObject));
        }

        [Test]
        public void ShouldReturnParent_because_doesNotHaveChildrenTag()
        {
            ITagFilter filter = new TagFilter(StubObjectTreeCreator.commonChildrenTag);
            filter.invertFilter = true;
            List<ITagFilter> filterList = new List<ITagFilter>() {filter};
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFilters(building, filterList);
            Assert.AreEqual(1, filteredObjects.Count);
            Assert.AreEqual(building, filteredObjects[0]);
        }

    }
}
