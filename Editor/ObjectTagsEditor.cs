using ReupVirtualTwin.models;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.webRequesters;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.editor
{
    [CustomEditor(typeof(ObjectTags))]
    public class ObjectTagsEditor : Editor
    {

        private ObjectTags objectTags;

        private SelectTagsSection selectTagsSection;

        private void OnEnable()
        {
            ITagsApiManager tagsApiManager = ObjectFinder.FindTagsApiManager();
            if (tagsApiManager.tagsApiConsumer == null)
            {
                tagsApiManager.tagsApiConsumer = new TagsApiConsumer(ObjectTags.tagsUrl);
            }
            selectTagsSection = new SelectTagsSection(tagsApiManager);
        }

        public override void OnInspectorGUI()
        {
            objectTags = (ObjectTags)target;
            objectTags.tags = selectTagsSection.selectedTags;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"), true, new GUILayoutOption[0]);
            ShowCurrentTags();
            EditorGUILayout.Space();
            selectTagsSection.ShowTagsToAdd();
            if (GUI.changed)
            {
                EditorUtility.SetDirty(objectTags);
            }
        }

        private void ShowCurrentTags()
        {
            EditorGUILayout.LabelField("Current tags:");
            List<Tag> tempTags = new List<Tag>(selectTagsSection.selectedTags);
            tempTags.ForEach(tag =>
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(tag.name);
                if (GUILayout.Button("Remove"))
                {
                    selectTagsSection.selectedTags.Remove(tag);
                }
                EditorGUILayout.EndHorizontal();
            });
        }


    }
}
