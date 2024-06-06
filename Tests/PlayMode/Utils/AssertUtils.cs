using NUnit.Framework;
using ReupVirtualTwin.helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssertUtils
{

    public static void AssertAllObjectsWithMeshRendererHaveMetaDataValue(List<GameObject> objects, string metaDataPath, string materialId)
    {
        List<string> objectsMaterialId = ObjectMetaDataUtils.GetStringMetaDataFromObjects(objects, metaDataPath);
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].GetComponent<MeshRenderer>() != null)
            {
                Assert.AreEqual(materialId, objectsMaterialId[i]);
            }
            else
            {
                Assert.IsNull(objectsMaterialId[i]);
            }
        }
    }

    public static void AssertAllAreNull<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Assert.IsNull(list[i]);
        }
    }

}
