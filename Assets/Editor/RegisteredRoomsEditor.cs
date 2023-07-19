using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ReUpVirtualTwin
{
    [CustomEditor(typeof(RegisteredRooms))]
    public class RegisteredRoomsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RegisteredRooms registeredRooms = (RegisteredRooms)target;
            GameObject[] rooms = GameObject.FindGameObjectsWithTag(TagsEnum.roomSelector);
            foreach(GameObject room in rooms)
            {
                var roomSelector = room.GetComponent<SpaceSelector>();
                registeredRooms.roomSelectors.Add(roomSelector);
                EditorGUILayout.LabelField(roomSelector.spaceName);
            }
        }
    }
}
