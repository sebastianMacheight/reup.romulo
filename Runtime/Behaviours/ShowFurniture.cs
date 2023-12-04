using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class ShowFurniture : MonoBehaviour
    {
        [SerializeField]
        GameObject furniture;

        public void toggleShowFurniture(bool toggle) {
            furniture.SetActive(toggle);
        }
    }
}