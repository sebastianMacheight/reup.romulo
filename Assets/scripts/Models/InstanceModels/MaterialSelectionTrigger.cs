using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelectionTrigger : MonoBehaviour
{
    public List<GameObject> materialObjects;
    [HideInInspector]
    public int[] materialIndexes;
    public List<Material> selectableMaterials;
}
