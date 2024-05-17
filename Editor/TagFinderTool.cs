using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ReupVirtualTwin.editor
{
    public class TagFinderTool : EditorWindow
    {
        [MenuItem("Reup Romulo/Tag Finder")]
        public static void ShowWindow()
        {
            GetWindow<TagFinderTool>("Tag Finder");
        }
    }
}
