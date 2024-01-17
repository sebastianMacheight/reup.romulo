using UnityEngine;
using ReupVirtualTwin.helpers;
using System;
using ReupVirtualTwin.behaviourInterfaces;

namespace ReupVirtualTwin.behaviours
{
    public class SetUpBuilding : MonoBehaviour , ISetUpBuilding
    {
        [SerializeField]
        GameObject building;
        private bool buildingSetup = false;
        private ITagSystemAssigner _tagSystemAssigner;

        event Action _onBuildingSetUp;
        public event Action onBuildingSetUp
        {
            add
            {
                if (buildingSetup)
                {
                    value();
                }
                else _onBuildingSetUp += value;
            }
            remove { _onBuildingSetUp -= value; }
        }

        void Start()
        {
            if (building != null)
            {
                AddCollidersToBuilding.AddColliders(building);
            }
            else
            {
                Debug.LogError("Building object not set up");
            }
            _onBuildingSetUp?.Invoke();
            buildingSetup = true;
        }

        public void AssignIdsToBuilding()
        {
            AssignIds.AssignToTree(building);
            Debug.Log("Ids added to tree");
        }
        public void RemoveIdsOfBuilding()
        {
            AssignIds.RemoveFromTree(building);
            Debug.Log("Ids removed from tree");
        }
        public void ResetIdsOfBuilding()
        {
            RemoveIdsOfBuilding();
            AssignIdsToBuilding();
            Debug.Log("Ids reseted from tree");
        }

        public void AddTagSystemToBuildingObjects()
        {
            if (_tagSystemAssigner == null)
            {
                _tagSystemAssigner = GetComponent<ITagSystemAssigner>();
            }
            _tagSystemAssigner.AssignTagSystemToTree(building);
            Debug.Log("tag script added to tree");
        }
    }
}