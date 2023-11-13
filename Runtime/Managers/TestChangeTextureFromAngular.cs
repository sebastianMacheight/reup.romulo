using ReupVirtualTwin.helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestChangeTextureFromAngular : MonoBehaviour
{
    public GameObject objectToChange;

    Texture2D texture;
    IMaterialChanger materialChanger;

    void Start()
    {
        materialChanger = GetComponent<IMaterialChanger>();
        //_materialsManager = ObjectFinder.FindMaterialsManager().GetComponent<MaterialsHelper>();
        //_materialsManager.SelectObjects(new List<GameObject> { objectToChange }, new int[] { 0 });
    }
    public IEnumerator TestChangeTexture(string url)
    {
        Debug.Log("la url en test es");
        Debug.Log(url);
        yield return StartCoroutine(LoadTextureFromUrl(url));
        Debug.Log("the texture");
        Debug.Log(texture);
        var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.SetTexture("_BaseMap", texture);
        materialChanger.SetNewMaterialToObjects(new List<GameObject> {objectToChange},
            new int[1] {0},
            material);
    }

    IEnumerator LoadTextureFromUrl (string url) {
        var image = UnityWebRequestTexture.GetTexture(url);
        yield return image.SendWebRequest();
        if (image.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(image.error);
        }
        else
        {
            texture = DownloadHandlerTexture.GetContent(image);
        }
    }

}
