using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class HeightMediator : MonoBehaviour, IMediator
    {
        private ICreateCollider _createCollider;
        private IMaintainHeight _maintainHeight;
        private IInitialSpawn _initialSpawn;

        public ICreateCollider createCollider { set { _createCollider = value; } }
        public IMaintainHeight maintainHeight { set { _maintainHeight = value; } }
        public IInitialSpawn initialSpawn { set { _initialSpawn = value; } }

        private float minHeight = 0.15f;

        [Range(0.15f, 3f)]
        public float characterHeight = 1.75f;

        private void Start()
        {
            updateHeight();
            _initialSpawn.Spawn();
        }

        public void Notify(ReupEvent eventName)
        {
            throw new System.NotImplementedException();
        }
        public void Notify<T>(ReupEvent eventName, T payload)
        {
            if (eventName == ReupEvent.addToCharacterHeight)
            {
                AddToHeight((float)(object)payload);
            }
        }
        private void updateHeight()
        {
            _createCollider.UpdateCollider(characterHeight);
            _maintainHeight.characterHeight = characterHeight;
        }
        private void AddToHeight(float heightDelta)
        {
            if (characterHeight + heightDelta > minHeight)
            {
                characterHeight += heightDelta;
                updateHeight();
            }
        }
    }
}
