using ReupVirtualTwin.behaviourInterfaces;
using UnityEditor;
using UnityEngine;

namespace Packages.reup.romulo.Tests.PlayMode.Mocks
{
    class MockSetUpBuilding : ISetUpBuilding
    {
        public event System.Action onBuildingSetUp;
    }
}