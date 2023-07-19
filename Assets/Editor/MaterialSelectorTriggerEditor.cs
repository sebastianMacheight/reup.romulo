using System;
using UnityEditor;
using UnityEngine;

namespace ReUpVirtualTwin
{

    [CustomEditor(typeof(MaterialSelectionTrigger))]
    public class MaterialSelectorTriggerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MaterialSelectionTrigger trigger = (MaterialSelectionTrigger)target;

            if (trigger.materialObject != null)
            {
                var materials = trigger.materialObject.GetComponent<Renderer>().sharedMaterials;
                if (materials.Length > 1)
                {
                    EditorGUILayout.HelpBox($"The object '{trigger.materialObject.name}' has more than one material assigned, select the material you want to change", MessageType.Warning, true);
                    string[] materialOptions = new string[materials.Length];
                    for (int i = 0; i < materialOptions.Length; i++)
                    {
                        materialOptions[i] = materials[i].name;
                    }
                    EditorGUI.BeginChangeCheck();
                    trigger.materialIndex = EditorGUILayout.Popup("Material to change", trigger.materialIndex, materialOptions);
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorUtility.SetDirty(trigger);
                    }
                }
            }
        }
    }
}
