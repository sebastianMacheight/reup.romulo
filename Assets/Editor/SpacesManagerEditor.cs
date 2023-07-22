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
            DrawDefaultInspector();
            SpacesManager spacesManager = (SpacesManager)target;
            GameObject[] spaces = GameObject.FindGameObjectsWithTag(TagsEnum.spaceSelector);
            if (spaces.Length != spacesManager.spaceSelectors.Count)
            {
                Debug.Log($"something is going on ... currently there are {spacesManager.spaceSelectors.Count}");
                spacesManager.UpdateSpaces();
                Debug.Log($"after something going on ... the number is {spacesManager.spaceSelectors.Count}");
            }
            EditorGUILayout.LabelField("List of spaces in the scene: ", EditorStyles.boldLabel);
            foreach (SpaceSelector spaceSelector in spacesManager.spaceSelectors)
            {
                    EditorGUILayout.LabelField($" - {spaceSelector.gameObject.name} ({spaceSelector.spaceName})");
            }
        }
    }
}
