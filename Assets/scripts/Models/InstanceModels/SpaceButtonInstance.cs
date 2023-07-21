using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceButtonInstance : MonoBehaviour
{
    [SerializeField]
    TMP_Text nameField;
    public string spaceName
    {
        get { return nameField.text; }
        set { nameField.text = value; }
    }
}
