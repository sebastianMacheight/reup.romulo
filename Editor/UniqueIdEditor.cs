using UnityEditor;
using ReupVirtualTwin.models;

[CustomEditor(typeof(UniqueId))]
public class UniqueIdEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UniqueId Identifier = (UniqueId)target;
        EditorGUILayout.TextField("copy and paste it!", Identifier.uniqueId);
    }
}
