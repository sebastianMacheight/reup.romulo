using UnityEngine;
using UnityEditor;
using ReupVirtualTwin.behaviours;

namespace ReupVirtualTwin.editor
{
    [CustomEditor(typeof(SetUpBuilding))]
    public class SetUpBuildingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SetUpBuilding setUpBuilding = (SetUpBuilding)target;

            if (GUILayout.Button("Add Ids to objects"))
            {
                setUpBuilding.AssignIdsToBuilding();
            }
            if (GUILayout.Button("Remove Ids from objects"))
            {
                setUpBuilding.RemoveIdsOfBuilding();
            }
            if (GUILayout.Button("Reset Ids from objects"))
            {
                setUpBuilding.ResetIdsOfBuilding();
            }
        }
    }
}
