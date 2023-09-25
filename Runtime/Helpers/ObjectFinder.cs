using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.helpers
{
    public static class ObjectFinder
    {
        /// <summary>
        /// Find the Object Pool
        /// </summary>
        /// <returns>ObjectPool</returns>
        public static IObjectPool FindObjectPool()
        {
            return GameObject.FindGameObjectWithTag(TagsEnum.objectPool).GetComponent<IObjectPool>();
        }

        public static IMaterialsContainerCreator FindMaterialsContainerCreator()
        {
            return GameObject.FindGameObjectWithTag(TagsEnum.extensionsTriggers).GetComponent<IMaterialsContainerCreator>();
        }

        public static GameObject FindCharacter()
        {
            return GameObject.FindGameObjectWithTag(TagsEnum.character);
        }

        public static GameObject FindMaterialsManager()
        {
            return GameObject.FindGameObjectWithTag(TagsEnum.materialsManager);
        }
        public static GameObject FindSpacesRecord()
        {
            return GameObject.FindGameObjectWithTag(TagsEnum.spacesRecord);
        }
    }
}
