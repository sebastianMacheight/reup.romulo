using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelectionTrigger : MonoBehaviour
{
    public GameObject materialObject;
    [HideInInspector]
    public int materialIndex;
    public List<Material> selectableMaterials;
}
