using UnityEditor;
using ReupVirtualTwin.models;

[CustomEditor(typeof(RegisteredIdentifier))]
public class RegisteredIdentifierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UniqueId Identifier = (RegisteredIdentifier)target;
        EditorGUILayout.TextField("copy and paste it!", Identifier.uniqueId);
    }
}
