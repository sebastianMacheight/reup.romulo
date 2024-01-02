using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class HeightMediator : MonoBehaviour, IHeightMediator
    {
        private ICreateCollider _createCollider;
        private IMaintainHeight _maintainHeight;

        public ICreateCollider createCollider { set { _createCollider = value; } }
        public IMaintainHeight maintainHeight { set { _maintainHeight = value; } }

        [Range(0.1f, 3f)]
        public float characterHeight = 1.75f;

        private void Start()
        {
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
            _createCollider.UpdateCollider(height);
            _maintainHeight.characterHeight = height;
        }
    }
}
