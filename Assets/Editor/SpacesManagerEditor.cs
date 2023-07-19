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
                spacesManager.spaceSelectors.Clear();
                foreach (GameObject room in spaces)
                {
                    var roomSelector = room.GetComponent<SpaceSelector>();
                    spacesManager.spaceSelectors.Add(roomSelector);
                }
            }
            EditorGUILayout.LabelField("List of spaces in the scene: ", EditorStyles.boldLabel);
            foreach (SpaceSelector spaceSelector in spacesManager.spaceSelectors)
            {
                    EditorGUILayout.LabelField($" - {spaceSelector.gameObject.name} ({spaceSelector.spaceName})");
            }
        }
    }
}
