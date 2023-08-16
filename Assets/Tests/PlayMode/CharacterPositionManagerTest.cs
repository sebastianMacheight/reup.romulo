using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterPositionManagerTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator SliceToTarget_should_success()
    {
        var character = new GameObject();
        var rb = character.AddComponent<Rigidbody>();
        rb.useGravity = false;
        var posManager = character.AddComponent<CharacterPositionManager>();

        //check original character's position
        Assert.AreEqual(Vector3.zero, character.transform.position);

        var targetPosition = new Vector3(10, 10, 10);

        posManager.SliceToTarget(targetPosition);

        yield return new WaitForSeconds(5);

        //check new character's position is REALLY close to target position
        Assert.LessOrEqual(Vector3.Distance(targetPosition, character.transform.position), 0.1f);
    }

    [UnityTest]
    public IEnumerator WalkToTarget_should_success()
    {
        var character = new GameObject();
        var rb = character.AddComponent<Rigidbody>();
        rb.useGravity = false;
        var posManager = character.AddComponent<CharacterPositionManager>();

        //check original character's position
        Assert.AreEqual(Vector3.zero, character.transform.position);

        var targetPosition = new Vector3(10, 10, 10);

        posManager.WalkToTarget(targetPosition);

        yield return new WaitForSeconds(5);

        var sameHeightTargetPosition = new Vector3(10, 0, 10);

        //check new character's position is close to target position
        Assert.LessOrEqual(Vector3.Distance(sameHeightTargetPosition, character.transform.position), 0.5f);
    }
}
