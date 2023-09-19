using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using ReupVirtualTwin.characterMovement;

public class CharacterPositionManagerTest : MonoBehaviour
{
    private GameObject character;
    private Rigidbody rb;
    private CharacterPositionManager posManager;

    [SetUp]
    public void SetUp()
    {
        character = new GameObject();
        rb = character.AddComponent<Rigidbody>();
        rb.useGravity = false;
        posManager = character.AddComponent<CharacterPositionManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(character);
    }

    [UnityTest]
    public IEnumerator SliceToTargetShouldSuccess()
    {
        //check original character's position
        Assert.AreEqual(Vector3.zero, character.transform.position);

        var targetPosition = new Vector3(10, 10, 10);

        posManager.SliceToTarget(targetPosition);

        yield return new WaitForSeconds(5);

        //check new character's position is REALLY close to target position
        Assert.LessOrEqual(Vector3.Distance(targetPosition, character.transform.position), 0.1f);
    }

    [UnityTest]
    public IEnumerator WalkToTargetShouldSuccess()
    {
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
