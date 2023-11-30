using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class HeightMediator : MonoBehaviour, IHeightMediator
    {
        [SerializeField]
        GameObject createColliderContainer;
        private ICreateCollider createCollider;
        [SerializeField]
        GameObject maintainheightContainer;
        private IMaintainHeight maintainHeight;

        [Range(0.1f, 3f)]
        public float characterHeight = 1.75f;

        private void Start()
        {
            createCollider = createColliderContainer.GetComponent<ICreateCollider>();
            maintainHeight = maintainheightContainer.GetComponent<IMaintainHeight>();

            updateHeight(characterHeight);
        }

        public void Notify(string eventName, float height)
        {
            if (eventName == "UpdateHeight")
            {
                updateHeight(height);
            }
        }
        private void updateHeight(float height)
        {
            createCollider.UpdateCollider(height);
            maintainHeight.characterHeight = height;
        }
    }
}
