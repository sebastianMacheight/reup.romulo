using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using System;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class HeightMediator : MonoBehaviour, IMediator
    {
        private ICharacterColliderController _colliderController;
        private IMaintainHeight _maintainHeight;
        private IInitialSpawn _initialSpawn;
        private Transform _ceilCheck;
        private LayerMask _buildingLayerMask;
        private float minHeight = 0.15f;
        private float _ceilCheckRadius = 0.12f;

        public ICharacterColliderController colliderController { set { _colliderController = value; } }
        public IMaintainHeight maintainHeight { set { _maintainHeight = value; } }
        public IInitialSpawn initialSpawn { set { _initialSpawn = value; } }
        public Transform ceilCheck { set =>  _ceilCheck = value; }
        public LayerMask buildingLayerMask { set =>  _buildingLayerMask = value; }

        [Range(0.15f, 3f)]
        public float characterHeight = 1.75f;

        private void Start()
        {
            updateHeight();
            _initialSpawn.Spawn();
        }

        public void Notify(ReupEvent eventName)
        {
            switch (eventName)
            {
                case ReupEvent.setCharacterHeight:
                    updateHeight();
                    break;
                default:
                    break;
            }
        }
        public void Notify<T>(ReupEvent eventName, T payload)
        {
            switch(eventName)
            {
                case ReupEvent.addToCharacterHeight:
                    AddToHeight((float)(object)payload);
                    break;
                default:
                    break;
            }
        }
        private void updateHeight()
        {
            _colliderController.UpdateCollider(characterHeight);
            _maintainHeight.characterHeight = characterHeight;
        }
        private void AddToHeight(float heightDelta)
        {
            Boolean minHeightGuard = characterHeight + heightDelta < minHeight;
            Boolean ceilGuard = Physics.CheckSphere(_ceilCheck.position, _ceilCheckRadius, _buildingLayerMask) && heightDelta > 0;
            if (minHeightGuard)
            {
                Debug.LogWarning($"character has reached it's mininum allowed height of {minHeight} m.");
                return;
            }
            if(ceilGuard)
            {
                Debug.LogWarning("character can not increase any further it's height because of ceil collision");
                return;
            }
            _colliderController.DestroyCollider();
            characterHeight += heightDelta;
            _maintainHeight.characterHeight = characterHeight;
        }
        private void OnDrawGizmosSelected()
        {
            if (_ceilCheck)
            {
                Gizmos.DrawWireSphere(_ceilCheck.position, _ceilCheckRadius);
            }
        }
    }
}
