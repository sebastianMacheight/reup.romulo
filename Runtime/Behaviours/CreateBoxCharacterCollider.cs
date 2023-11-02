using UnityEngine;

namespace ReupVirtualTwin.behaviours
{

    public class CreateBoxCharacterCollider : MonoBehaviour
    {
        [SerializeField]
        GameObject character;

        float FREE_SPACE_BELOW_IN_METERS = 1f;
        float COLLIDER_FRONT_IN_METERS = 0.02f;
        float COLLIDER_SIDE_IN_METERS = 0.2f;

        private void Start()
        {
            var collider = character.AddComponent<BoxCollider>();
            var colliderHeight = MaintainHeight.CHARACTER_HEIGHT - FREE_SPACE_BELOW_IN_METERS;
            var colliderYCenter = -1 * colliderHeight / 2;
            collider.size = new Vector3(COLLIDER_FRONT_IN_METERS, colliderHeight, COLLIDER_SIDE_IN_METERS);
            collider.center = new Vector3(0, colliderYCenter, 0);
        }
    }
}
