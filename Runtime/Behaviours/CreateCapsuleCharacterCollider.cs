using UnityEngine;

namespace ReupVirtualTwin.behaviours
{

    public class CreateCapsuleCharacterCollider : MonoBehaviour
    {
        [SerializeField]
        GameObject character;
        [SerializeField]
        PhysicMaterial physicMaterial;

        float FREE_SPACE_BELOW_IN_METERS = 1f;
        float COLLIDER_RADIUS_IN_METERS = 0.05f;

        private void Start()
        {
            var collider = character.AddComponent<CapsuleCollider>();
            var colliderHeight = HeightKeeper.CHARACTER_HEIGHT - FREE_SPACE_BELOW_IN_METERS;
            var colliderYCenter = -1 * colliderHeight / 2;
            collider.radius = COLLIDER_RADIUS_IN_METERS;
            collider.height = colliderHeight;
            collider.center = new Vector3(0, colliderYCenter, 0);
            collider.material = physicMaterial;
        }

    }
}
