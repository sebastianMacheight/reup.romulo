using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ReUpVirtualTwin.Helpers
{
    public static class ObjectFinder
    {
        /// <summary>
        /// Find the Object Pool
        /// </summary>
        /// <returns>ObjectPool</returns>
        public static IObjectPool FindObjectPool()
        {
            return GameObject.FindGameObjectWithTag("ObjectPool").GetComponent<IObjectPool>();
        }

        /// <summary>
        /// Find the ok button of a parent
        /// </summary>
        /// <param name="parent">Parent where to look for the ok button</param>
        /// <returns>OkButton</returns>
        public static Button FindOkButton(GameObject parent)
        {
            var buttons = parent.GetComponentsInChildren<Button>();
            var button = buttons.FirstOrDefault(btn => btn.CompareTag("OkButton"));
            return button;
        }

        /// <summary>
        /// Finds the dragmanager
        /// </summary>
        /// <returns>the dragmanager</returns>
        public static DragManager FindDragManager()
        {
            return GameObject.FindAnyObjectByType<DragManager>();
        }

        public static ExtensionsManager FindExtensionManager()
        {
            return GameObject.FindAnyObjectByType<ExtensionsManager>();
        }
        public static GameObject FindExtensionsTrigger()
        {
            return GameObject.FindGameObjectWithTag("ExtensionsTriggers");
        }

        public static MaterialsManager FindMaterialsManager()
        {
            return GameObject.FindAnyObjectByType<MaterialsManager>();
        }
        public static SpacesManager FindSpacesManager()
        {
            return GameObject.FindAnyObjectByType<SpacesManager>();
        }
    }
}
