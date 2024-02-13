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
        private float _ceilCheckRadius = 0.1f;

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
            _colliderController.UpdateCollider(characterHeight);
            _maintainHeight.characterHeight = characterHeight;
        }
        private void AddToHeight(float heightDelta)
        {
            Boolean minHeightGuard = characterHeight + heightDelta < minHeight;
            Boolean ceilGuard = Physics.CheckSphere(_ceilCheck.position, _ceilCheckRadius, _buildingLayerMask) && heightDelta > 0;
            if (!minHeightGuard && !ceilGuard)
            {
                characterHeight += heightDelta;
                _colliderController.DestroyCollider();
                _maintainHeight.characterHeight = characterHeight;
            }
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
