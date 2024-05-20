using ReupVirtualTwin.managerInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.controllers;

namespace ReupVirtualTwin.editor
{
    public class TagFinderTool : EditorWindow
    {
        private SelectTagsSection selectTagsSection;
        private List<ITagFilter> tagFilters = new List<ITagFilter>();

        [MenuItem("Reup Romulo/Tag Finder")]
        public static void ShowWindow()
        {
            GetWindow<TagFinderTool>("Tag Finder");
        }

        private void OnEnable()
        {
            ITagsApiManager tagsApiManager = TagsApiManagerEditorFinder.FindTagApiManager();
            selectTagsSection = new SelectTagsSection(tagsApiManager);
            selectTagsSection.onTagAdded = OnTagAdded;
        }
        void OnGUI()
        {
            ShowTagsFilters();
            EditorGUILayout.Space();
            selectTagsSection.ShowTagsToAdd();
        }
        private void ShowTagsFilters()
        {
            float totalWidth = EditorGUIUtility.currentViewWidth;
            float filterNameWidth = totalWidth * 0.6f;
            float removeFilterButtonWidth = totalWidth * 0.2f;
            float invertFilterToggleWidth = totalWidth * 0.2f;
            List<ITagFilter> tempFilters = new List<ITagFilter>(tagFilters);
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Current filters", GUILayout.Width(filterNameWidth));
                EditorGUILayout.LabelField("Remove filter", GUILayout.Width(removeFilterButtonWidth));
                EditorGUILayout.LabelField("Invert filter", GUILayout.Width(invertFilterToggleWidth));
            EditorGUILayout.EndHorizontal();
            tempFilters.ForEach(filter =>
            {
                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(filter.displayText, GUILayout.Width(filterNameWidth));
                    if (GUILayout.Button("Remove filter", GUILayout.Width(removeFilterButtonWidth)))
                    {
                        filter.RemoveFilter();
                    }
                    filter.invertFilter = EditorGUILayout.Toggle(filter.invertFilter, GUILayout.Width(invertFilterToggleWidth));
                EditorGUILayout.EndHorizontal();
            });
        }
        private void OnTagAdded(Tag tag)
        {
            ITagFilter tagFilter = new TagFilter(tag);
            tagFilters.Add(tagFilter);
            tagFilter.onRemoveFilter = () =>
            {
                selectTagsSection.selectedTags.Remove(tag);
                tagFilters.Remove(tagFilter);
            };
        }
    }
}
