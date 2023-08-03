using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ReUpVirtualTwin
{
    [CustomEditor(typeof(SpacesManager))]
    public class SpacesManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SpacesManager spacesManager = (SpacesManager)target;
            GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
            if (spaces.Length != spacesManager.spaceSelectors.Count)
            {
                spacesManager.UpdateSpaces();
            }
            EditorGUILayout.LabelField("List of spaces in the scene: ", EditorStyles.boldLabel);
            foreach (SpaceJumpPoint spaceSelector in spacesManager.spaceSelectors)
            {
                    EditorGUILayout.LabelField($" - {spaceSelector.gameObject.name} ({spaceSelector.spaceName})");
            }


            GameObject[] spacePlanes = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelectorPlane);
            if (spacePlanes.Length != spacesManager.spaceSelectorPlanes.Count)
            {
                spacesManager.UpdatePlanes();
            }
            EditorGUILayout.Space(10f);

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
