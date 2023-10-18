using System.Linq;
using UnityEditor;
using UnityEngine;
using ReupVirtualTwin.models;

[CustomEditor(typeof(MaterialSelectionTrigger))]
public class MaterialSelectorTriggerEditor : Editor
{
    MaterialSelectionTrigger trigger;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        trigger = (MaterialSelectionTrigger)target;

        if (trigger.materialObjects.Count != 0)
        {
            if (trigger.objectsMaterialIndexes.Length != trigger.materialObjects.Count)
                trigger.objectsMaterialIndexes = new int[trigger.materialObjects.Count];
            EditorGUI.BeginChangeCheck();
            foreach (var (obj, j) in trigger.materialObjects.Select((v, i) => (v, i)))
            {
                if (obj != null)
                {
                    var materials = obj.GetComponent<Renderer>().sharedMaterials;
                    if (materials.Length > 1)
                    {
                        var warningMessage = $"The object '{obj.name}' has more than one material assigned, select the material you want to change";
                        EditorGUILayout.HelpBox(warningMessage, MessageType.Warning, true);
                        string[] materialOptions = new string[materials.Length];
                        for (int i = 0; i < materialOptions.Length; i++)
                        {
                            materialOptions[i] = materials[i].name;
                        }
                        var infoMessage = $"Material to change for {obj.name}";
                        trigger.objectsMaterialIndexes[j] = EditorGUILayout.Popup(infoMessage,
                                                                                  trigger.objectsMaterialIndexes[j],
                                                                                  materialOptions);
                    }
                    checkIndexOutOfMaterialsArray(j, materials);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(trigger);
            }
        }
    }
    private void checkIndexOutOfMaterialsArray(int index, Material[] materials)
    {
        if (trigger.objectsMaterialIndexes[index] >= materials.Length)
        {
            trigger.objectsMaterialIndexes[index] = 0;
        }
    }
}
