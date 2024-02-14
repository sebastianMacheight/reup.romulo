using ReupVirtualTwin.behaviourInterfaces;
using UnityEditor;
using UnityEngine;

namespace Packages.reup.romulo.Tests.PlayMode.Mocks
{
    class MockSetUpBuilding : ISetUpBuilding
    {
        public event System.Action onBuildingSetUp;

        public void AddTagSystemToBuildingObjects()
        {
            throw new System.NotImplementedException();
        }

        public void AssignIdsToBuilding()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveIdsOfBuilding()
        {
            throw new System.NotImplementedException();
        }

        public void ResetIdsOfBuilding()
        {
            throw new System.NotImplementedException();
        }
    }
}