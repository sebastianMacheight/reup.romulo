using ReupVirtualTwin.models;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using ReupVirtualTwin.enums;

[CustomEditor(typeof(ObjectTags))]
public class ObjectTagsEditor : Editor
    {
    private string searchTagText = "";
    private SerializedProperty tagsProperty;

    private void OnEnable()
    {
        tagsProperty = serializedObject.FindProperty("tags");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ShowCurrentTags();
        EditorGUILayout.Space();
        ShowTagsToAdd();
        serializedObject.ApplyModifiedProperties();
    }

    private void ShowCurrentTags()
    {
        EditorGUILayout.LabelField("Current tags:");
        for (int i = tagsProperty.arraySize - 1; i >= 0; i--)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(tagsProperty.GetArrayElementAtIndex(i).stringValue);
            if (GUILayout.Button("Remove"))
            {
                tagsProperty.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void ShowTagsToAdd()
    {
        searchTagText = EditorGUILayout.TextField("Search for tag to add:", searchTagText);
        EditorGUILayout.Space();
        List<string> allTags = GetAllTags().Where(tag => TagContainsText(tag, searchTagText) && !IsTagAlredyPresent(tag)).ToList();
        foreach (string tag in allTags)
        {
            if (GUILayout.Button(tag))
            {
                AddTagIfNotPresent(tag);
            }
        }
    }

    private bool TagContainsText(string tag, string text)
    {
        return tag.ToLower().Contains(text.ToLower());
    }

    private void AddTagIfNotPresent(string tag)
    {
        if (!IsTagAlredyPresent(tag))
        {
            tagsProperty.arraySize++;
            tagsProperty.GetArrayElementAtIndex(tagsProperty.arraySize - 1).stringValue = tag;
        }
    }

    private bool IsTagAlredyPresent(string tag)
    {
        bool isPresent = false;
        for (int i = 0; i < tagsProperty.arraySize; i++)
        {
            if (tagsProperty.GetArrayElementAtIndex(i).stringValue == tag)
            {
                isPresent = true;
                break;
            }
        }
        return isPresent;
    }


    private List<string> GetAllTags()
    {
        return new List<string>() {
            ObjectTag.SELECTABLE.ToString(),
            ObjectTag.DELETABLE.ToString(),
            ObjectTag.TRANSFORMABLE.ToString(),
        };
    }
}
