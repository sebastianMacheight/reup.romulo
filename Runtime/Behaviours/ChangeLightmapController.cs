using UnityEngine;

public class ChangeLightmapController : MonoBehaviour
{

    ChangeLightmap lightmapChanger = new ChangeLightmap();
    [SerializeField] string resourceFolderFurniture;
    [SerializeField] string resourceFolderNoFurniture;

    void OnEnable()
    {
        lightmapChanger.Load(resourceFolderFurniture);
    }

    private void OnDisable()
    {
        lightmapChanger.Load(resourceFolderNoFurniture);
    }

}