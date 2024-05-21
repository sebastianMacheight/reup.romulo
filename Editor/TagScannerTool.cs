using ReupVirtualTwin.managerInterfaces;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.controllers;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.behaviourInterfaces;

namespace ReupVirtualTwin.editor
{
    public class TagScannerTool : EditorWindow
    {
        private SelectTagsSection selectTagsSection;
        private List<ITagFilter> tagFilters = new List<ITagFilter>();
        private IBuildingGetterSetter setupBuilding;
        private SceneVisibilityManager sceneVisibilityManager;

        [MenuItem("Reup Romulo/Tag Scanner")]
        public static void ShowWindow()
        {
            GetWindow<TagScannerTool>("Tag Scanner");
        }

        private async void OnEnable()
        {
            ITagsApiManager tagsApiManager = TagsApiManagerEditorFinder.FindTagApiManager();
            selectTagsSection = await SelectTagsSection.Create(tagsApiManager);
            selectTagsSection.onTagAdded = OnTagAdded;
            setupBuilding = ObjectFinder.FindSetupBuilding().GetComponent<IBuildingGetterSetter>();
            sceneVisibilityManager = SceneVisibilityManager.instance;
        }
        void OnGUI()
        {
            ShowTagsFilters();
            EditorGUILayout.Space();
            selectTagsSection.ShowTagsToAdd();
            if (GUILayout.Button("Apply filters"))
            {
                ApplyFilters();
            }
        }
        private void ApplyFilters()
        {
            GameObject building = setupBuilding.building;
            if (building == null)
            {
                Debug.LogError("No building found");
                return;
            }
            List<GameObject> filteredObjects = TagFiltersApplier.ApplyFilters(building, tagFilters);
            sceneVisibilityManager.Hide(building, true);
            for (int i = 0; i < filteredObjects.Count; i++)
            {
                sceneVisibilityManager.Show(filteredObjects[i], true);
            }
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
