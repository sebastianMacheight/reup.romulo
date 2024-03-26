using ReupVirtualTwin.models;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using ReupVirtualTwin.enums;
using UnityEngine.UIElements;

[CustomEditor(typeof(ObjectTags))]
public class ObjectTagsEditor : Editor
    {
    private string searchTagText = "";
    private SerializedProperty tagsProperty;
    private Vector2 scrollPosition;
    private int TAG_BUTTON_HEIGHT = 18;
    private int MAX_BUTTONS_IN_SCROLL_VIEW = 2;
    private int UNITY_BUTTON_MARGIN = 2; // This is a variable obtained by trial and error

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
        int scrollHeight = MAX_BUTTONS_IN_SCROLL_VIEW * (TAG_BUTTON_HEIGHT + UNITY_BUTTON_MARGIN);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(scrollHeight));
        List<string> allTags = GetAllTags().Where(tag => TagContainsText(tag, searchTagText) && !IsTagAlredyPresent(tag)).ToList();
        foreach (string tag in allTags)
        {
            if (GUILayout.Button(tag, GUILayout.Height(TAG_BUTTON_HEIGHT)))
            {
                AddTagIfNotPresent(tag);
            }
        }
        EditorGUILayout.EndScrollView();
        if(IsUserAtTheBottomOfScrollView(allTags.Count(), scrollPosition.y))
        {
            Debug.Log("User is at the bottom of the scroll view");
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
            ObjectTag.TRANSFORMABLE.ToString(),
            ObjectTag.DELETABLE.ToString(),
        };
    }
    private bool IsUserAtTheBottomOfScrollView(int numberOfTags, float scrollY)
    {
        int maxScroll = 
            (numberOfTags - MAX_BUTTONS_IN_SCROLL_VIEW) * (TAG_BUTTON_HEIGHT + UNITY_BUTTON_MARGIN) +
            UNITY_BUTTON_MARGIN;
        int bottomThresholdInPixels = 100;
        if (scrollY >= maxScroll - bottomThresholdInPixels)
        {
            return true;
        }
        return false;
    }
}
