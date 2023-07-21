using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ReUpVirtualTwin.Helpers;

public class SpaceButtonInstance : MonoBehaviour
{
    public SpaceSelector spaceSelector;
    CharacterPositionManager characterPositionManager;
    public string spaceName
    {
        get { return nameField.text; }
        set { nameField.text = value; }
    }

    [SerializeField]
    TMP_Text nameField;

    private void Start()
    {
        characterPositionManager = ObjectFinder.FindCharacterPositionManager();
    }

    public void GoToSpace()
    {
        characterPositionManager.SliceToTarget(spaceSelector.transform.position);
    }
}
