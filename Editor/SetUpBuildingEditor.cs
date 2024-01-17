using UnityEngine;
using UnityEditor;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.behaviours;

namespace ReupVirtualTwin.editor
{
    [CustomEditor(typeof(SetUpBuilding))]
    public class SetUpBuildingEditor : Editor
    {
        bool showIdsOptions = false;
        bool showTagsOptions = false;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ISetUpBuilding setUpBuilding = (ISetUpBuilding)target;

            showIdsOptions = EditorGUILayout.Foldout(showIdsOptions, "Objects Ids");
            if (showIdsOptions)
            {
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
            showTagsOptions = EditorGUILayout.Foldout(showTagsOptions, "Objects Ids");
            if (showTagsOptions)
            {
                if (GUILayout.Button("Add tag system to objects"))
                {
                    setUpBuilding.AddTagSystemToBuildingObjects();
                }
            }
        }
    }
}
