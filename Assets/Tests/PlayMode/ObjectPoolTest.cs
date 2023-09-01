using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ReupVirtualTwin.Helpers;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectPoolTest
{
    [UnityTest]
    public IEnumerator Pool_unpool_visibleObject_should_success()
    {
        var obj = new GameObject();
        var pool = new GameObject().AddComponent<ObjectPool>();
        //check object is active
        Assert.IsTrue(obj.activeSelf);
        yield return null;
        pool.PoolObject(obj);
        //check object is not active
        Assert.IsFalse(obj.activeSelf);
        yield return null;
        pool.GetObjectFromPool(obj.name);
        //check object is active again
        Assert.IsTrue(obj.activeSelf);
    }

    [UnityTest]
    public IEnumerator Unpool_newObject_noParentArgument_should_fail()
    {
        var pool = new GameObject().AddComponent<ObjectPool>();
        Assert.Throws<InvalidOperationException>(() => pool.GetObjectFromPool("name of object"));
        yield return null;
    }

    [UnityTest]
    public IEnumerator Unpool_pool_newObject_should_success()
    {
        var parentObject = new GameObject();
        var pool = new GameObject().AddComponent<ObjectPool>();
        var prefab = new GameObject("object's name");
        pool.PrefabsForPool = new List<GameObject>
        {
            prefab
        };

        //create prefabInstance
        var prefabInstance = pool.GetObjectFromPool(prefab.name, parentObject.transform);

        //check prefabInstance's parent
        Assert.AreEqual(parentObject, prefabInstance.transform.parent.gameObject);
        //check prefabInstance is a new instance
        Assert.AreNotEqual(prefab, prefabInstance);
        //check prefabInstance has same name af prefab
        Assert.AreEqual(prefab.name, prefabInstance.name);
        //check prefabInstance is active
        Assert.IsTrue(prefabInstance.activeSelf);

        //pool object
        pool.PoolObject(prefabInstance);

        //check prefabInstance is inactive
        Assert.IsFalse(prefabInstance.activeSelf);

        //unpool object again
        var prefabInstance2 = pool.GetObjectFromPool(prefab.name);

        //check prefabInstance is active
        Assert.IsTrue(prefabInstance.activeSelf);
        Assert.IsTrue(prefabInstance2.activeSelf);
        Assert.AreEqual(prefabInstance, prefabInstance2);

        //create another prefabInstance
        var prefabInstance3 = pool.GetObjectFromPool(prefab.name, parentObject.transform);

        //check prefabInstance3 is indeed a new gameobject
        Assert.AreNotEqual(prefabInstance, prefabInstance3);
            

        yield return null;
    }
}