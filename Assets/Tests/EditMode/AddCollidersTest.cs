using NUnit.Framework;
using ReupVirtualTwin.Helpers;
using UnityEngine;

public class AddCollidersTest
{
    [Test]
    public void AddColliders()
    {
        //Create objects
        var parentObject = new GameObject();
        for (int i = 0; i < 10; i++)
        {
            var childObject = new GameObject();
            childObject.transform.SetParent(parentObject.transform);
        }

        //Check colliders are not there
        Assert.IsNull(parentObject.GetComponent<Collider>());
        foreach (Transform childTransform in parentObject.transform)
        {
            Assert.IsNull(childTransform.gameObject.GetComponent<Collider>());
        }

        //Add colliders
        AddCollidersToBuilding.AddColliders(parentObject);

        //Check colliders are there
        Assert.IsNotNull(parentObject.GetComponent<Collider>());
        foreach (Transform childTransform in parentObject.transform)
        {
            Assert.IsNotNull(childTransform.gameObject.GetComponent<Collider>());
        }
    }
}
