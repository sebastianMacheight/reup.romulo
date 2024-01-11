using UnityEngine;
using ReupVirtualTwin.helpers;
using System;

namespace ReupVirtualTwin.behaviours
{
    public class SetUpBuilding : MonoBehaviour
    {
        [SerializeField]
        GameObject building;
        private bool buildingSetup = false;

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
        }
        public void RemoveIdsOfBuilding()
        {
            AssignIds.RemoveFromTree(building);
        }
        public void ResetIdsOfBuilding()
        {
            RemoveIdsOfBuilding();
            AssignIdsToBuilding();
        }

    }
}