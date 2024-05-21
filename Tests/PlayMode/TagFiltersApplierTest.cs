using UnityEngine;
using NUnit.Framework;

using ReupVirtualTwin.controllers;
using ReupVirtualTwin.controllerInterfaces;
using System.Collections.Generic;

namespace ReupVirtualTwinTests.controllers
{
    public class TagFiltersApplierTest : MonoBehaviour
    {
        GameObject building;

        [TearDown]
        public void TearDown()
        {
            Destroy(building);
        }
        [Test]
        public void ShouldGetParent()
        {
            building = StubObjectTreeCreator.CreateMockBuilding();
            ITagFilter filterparent = new TagFilter(StubObjectTreeCreator.parentTags[0]);
            List<ITagFilter> filterList = new List<ITagFilter>() { filterparent };
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFiltersToTree(building, filterList);
            Assert.AreEqual(1, filteredObjects.Count);
            Assert.AreEqual(building, filteredObjects[0]);
        }
        [Test]
        public void ShouldGetEmptyGameObjectList()
        {
            building = StubObjectTreeCreator.CreateMockBuilding();
            ITagFilter filterparent = new TagFilter(StubObjectTreeCreator.parentTags[0]);
            ITagFilter filterParentInverse = new TagFilter(StubObjectTreeCreator.parentTags[0]);
            filterParentInverse.invertFilter = true;
            List<ITagFilter> filterList = new List<ITagFilter>() { filterparent, filterParentInverse };
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFiltersToTree(building, filterList);
            Assert.AreEqual(0, filteredObjects.Count);
        }

        [Test]
        public void ShouldGetChildrenObjects()
        {
            building = StubObjectTreeCreator.CreateMockBuilding();
            List<ITagFilter> filterList = new List<ITagFilter>() {new TagFilter(StubObjectTreeCreator.commonChildrenTag)};
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFiltersToTree(building, filterList);
            Assert.AreEqual(2, filteredObjects.Count);
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(0).gameObject));
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(1).gameObject));
        }

        [Test]
        public void ShouldReturn_ObjectsWithTagX()
        {
            building = StubObjectTreeWithTagAtDifferentLevelsCreator.CreateMockObjectWithArbitraryTagAtSecondAndThirdLevel();
            ITagFilter filter = new TagFilter(StubObjectTreeWithTagAtDifferentLevelsCreator.tagX);
            List<ITagFilter> filterList = new List<ITagFilter>() {filter};
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFiltersToTree(building, filterList);
            for(int i = 0; i < filteredObjects.Count; i++)
            {
                Debug.Log("a name");
                Debug.Log(filteredObjects[i].name);
            }
            Assert.AreEqual(2, filteredObjects.Count);
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(0).GetChild(0).gameObject));
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(1).gameObject));
        }

        [Test]
        public void ShouldReturn_ObjectsWithoutTagX_and_WithoutChildrenWithTagX()
        {
            building = StubObjectTreeWithTagAtDifferentLevelsCreator.CreateMockObjectWithArbitraryTagAtSecondAndThirdLevel();
            ITagFilter filter = new TagFilter(StubObjectTreeWithTagAtDifferentLevelsCreator.tagX);
            filter.invertFilter = true;
            List<ITagFilter> filterList = new List<ITagFilter>() {filter};
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFiltersToTree(building, filterList);
            for(int i = 0; i < filteredObjects.Count; i++)
            {
                Debug.Log("a name");
                Debug.Log(filteredObjects[i].name);
            }
            Assert.AreEqual(2, filteredObjects.Count);
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(0).GetChild(1).gameObject));
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(2).gameObject));
        }

        [Test]
        public void ShouldReturn_ObjectsWithTagXAndY()
        {
            building = StubObjectTreeWithTagAtDifferentLevelsCreator.CreateMockObjectWithArbitraryTagAtSecondAndThirdLevel();
            ITagFilter filterX = new TagFilter(StubObjectTreeWithTagAtDifferentLevelsCreator.tagX);
            ITagFilter filterY = new TagFilter(StubObjectTreeWithTagAtDifferentLevelsCreator.tagY);
            List<ITagFilter> filterList = new List<ITagFilter>() {filterX, filterY};
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFiltersToTree(building, filterList);
            Assert.AreEqual(1, filteredObjects.Count);
            Assert.IsTrue(filteredObjects.Contains(building.transform.GetChild(1).gameObject));
        }


    }
}
