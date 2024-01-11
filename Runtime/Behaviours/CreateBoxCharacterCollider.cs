using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class CreateBoxCharacterCollider : MonoBehaviour, ICreateCollider

    {
        [SerializeField]
        GameObject character;

        float FREE_SPACE_BELOW_IN_METERS = 1f;
        float MIN_COLLIDER_HEIGHT = 0.05f;
        float COLLIDER_FRONT_IN_METERS = 0.02f;
        float COLLIDER_SIDE_IN_METERS = 0.2f;

        public bool UpdateCollider(float characterHeight)
        {
            if (IsCharacterTooLow(characterHeight))
            {
                return false;
            }
            DestroyCollider();
            return CreateCollider(characterHeight);
        }

        private void DestroyCollider()
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                Destroy(collider);
            }
        }

        private bool CreateCollider(float characterHeight)
        {
            BoxCollider collider = character.AddComponent<BoxCollider>();
            float colliderHeight = characterHeight - FREE_SPACE_BELOW_IN_METERS;
            if (colliderHeight < MIN_COLLIDER_HEIGHT)
            {
                colliderHeight = MIN_COLLIDER_HEIGHT;
            }
            float colliderYCenter = -1 * colliderHeight / 2;
            collider.size = new Vector3(COLLIDER_FRONT_IN_METERS, colliderHeight, COLLIDER_SIDE_IN_METERS);
            collider.center = new Vector3(0, colliderYCenter, 0);
            return true;
        }

        private bool IsCharacterTooLow(float characterHeight)
        {
            if (characterHeight < MIN_COLLIDER_HEIGHT)
            {
                return true;
            }
            return false;
        }
    }
}
