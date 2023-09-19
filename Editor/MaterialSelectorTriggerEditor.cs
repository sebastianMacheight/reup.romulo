using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ReupVirtualTwin.models;
using ReupVirtualTwin.helpers;


[CustomEditor(typeof(MaterialSelectionTrigger))]
public class MaterialSelectorTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MaterialSelectionTrigger trigger = (MaterialSelectionTrigger)target;

        if (trigger.materialObjects.Count != 0)
        {
            if (trigger.materialIndexes.Length != trigger.materialObjects.Count)
                trigger.materialIndexes = new int[trigger.materialObjects.Count];
            EditorGUI.BeginChangeCheck();
            foreach (var (obj, j) in trigger.materialObjects.Select((v, i) => (v, i)))
            {
                if (obj != null)
                {
                    var materials = obj.GetComponent<Renderer>().sharedMaterials;
                    if (materials.Length > 1)
                    {
                        EditorGUILayout.HelpBox($"The object '{obj.name}' has more than one material assigned, select the material you want to change", MessageType.Warning, true);
                        string[] materialOptions = new string[materials.Length];
                        for (int i = 0; i < materialOptions.Length; i++)
                        {
                            materialOptions[i] = materials[i].name;
                        }
                        trigger.materialIndexes[j] = EditorGUILayout.Popup($"Material to change for {obj.name}", trigger.materialIndexes[j], materialOptions);
                    }
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(trigger);
            }
        }
    }
}
