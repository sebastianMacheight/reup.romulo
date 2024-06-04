using NUnit.Framework;
using ReupVirtualTwin.helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace ReupVirtualTwinTests.helpers
{
    public class GameObjectUtilsTest : MonoBehaviour
    {
        GameObject parent0;
        GameObject child0;
        GameObject grandchild0;
        GameObject parent1;
        GameObject child1;
        GameObject grandchild1;

        [SetUp]
        public void SetUp()
        {
            parent0 = new GameObject("parent0");
            child0 = new GameObject("child0");
            child0.transform.parent = parent0.transform;
            grandchild0 = new GameObject("grandchild0");
            grandchild0.transform.parent = child0.transform;

            parent1 = new GameObject("parent1");
            child1 = new GameObject("child1");
            child1.transform.parent = parent1.transform;
            grandchild1 = new GameObject("grandchild1");
            grandchild1.transform.parent = child1.transform;
        }

        [TearDown]
        public void TearDown()
        {
            Destroy(parent0);
            Destroy(child0);
            Destroy(grandchild0);
            Destroy(parent1);
            Destroy(child1);
            Destroy(grandchild1);
        }


        [Test]
        public void ShouldFindParentInParent()
        {
            Assert.IsTrue(GameObjectUtils.IsPartOf(parent0, parent0));
        }

        [Test]
        public void ShouldFindChildInParent()
        {
            Assert.IsTrue(GameObjectUtils.IsPartOf(parent0, child0));
        }

        [Test]
        public void ShouldFindGrandChildInParent()
        {
            Assert.IsTrue(GameObjectUtils.IsPartOf(parent0, grandchild0));
        }

        [Test]
        public void ShouldNotFindParentInChild()
        {
            Assert.IsFalse(GameObjectUtils.IsPartOf(child0, parent0));
        }

        [Test]
        public void ShouldNotFindGrandChild1InParent0()
        {
            Assert.IsFalse(GameObjectUtils.IsPartOf(parent0, grandchild1));
        }

        [Test]
        public void ShouldFindGrandChildInParents()
        {
            List<GameObject> parents = new List<GameObject> { parent0, parent1 };
            Assert.IsTrue(GameObjectUtils.IsPartOf(parents, grandchild0));
        }
        [Test]
        public void ShouldNotFindParentInGrandchildren()
        {
            List<GameObject> grandchildren = new List<GameObject> { grandchild0, grandchild1 };
            Assert.IsFalse(GameObjectUtils.IsPartOf(grandchildren, parent0));
        }

    }
}
