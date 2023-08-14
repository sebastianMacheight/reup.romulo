using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ReUpVirtualTwin
{
    [CustomEditor(typeof(SpacesManager))]
    public class SpacesManagerEditor : Editor
    {

        bool ListCheck<T>(List<T> list, int expectedLength)
        {
            if (list.Count != expectedLength)
            {
                return true;
            }
            if (list.Count > 0 && list[0] == null)
            {
                return true;
            }
            return false;
        }

        public override void OnInspectorGUI()
        {
            SpacesManager spacesManager = (SpacesManager)target;
            GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
            if (ListCheck<SpaceJumpPoint>(spacesManager.spaceSelectors, spaces.Length))
            {
                spacesManager.UpdateSpaces();
            }
            EditorGUILayout.LabelField("List of spaces in the scene: ", EditorStyles.boldLabel);
            foreach (SpaceJumpPoint spaceSelector in spacesManager.spaceSelectors)
            {
                EditorGUILayout.LabelField($" - {spaceSelector.gameObject.name} ({spaceSelector.spaceName})");
            }

            EditorGUILayout.Space(10f);

            GameObject[] spacePlanes = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelectorPlane);
            if (ListCheck<FloorPlane>(spacesManager.spaceSelectorPlanes, spacePlanes.Length))
            {
                Debug.Log("updateing planes");
                spacesManager.UpdatePlanes();
            }

            EditorGUILayout.LabelField("List of spaces floor planes in the scene: ", EditorStyles.boldLabel);
            foreach (FloorPlane spaceSelectorPlane in spacesManager.spaceSelectorPlanes)
            {
                EditorGUILayout.LabelField($" - {spaceSelectorPlane.gameObject.name} ({spaceSelectorPlane.planeName})");
            }
            EditorGUILayout.Space(10f);

            spacesManager.drawSpacesGizmos =  EditorGUILayout.Toggle("Draw spaces gizmos", spacesManager.drawSpacesGizmos);
            spacesManager.floorPlanesLength = EditorGUILayout.IntField("Floor planes length", spacesManager.floorPlanesLength);
            spacesManager.gizmoGridSize = EditorGUILayout.IntSlider("Floor plane grid size", spacesManager.gizmoGridSize, 1, 10);

            DrawDefaultInspector();
        }
    }
}
