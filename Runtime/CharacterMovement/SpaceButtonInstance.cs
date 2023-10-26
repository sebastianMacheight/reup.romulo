using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.characterMovement
{
    public class SpaceButtonInstance : MonoBehaviour
    {
        public SpaceJumpPoint spaceSelector;

        CharacterPositionManager _characterPositionManager;
        SpacesRecord _spacesRecord;
        public string spaceName
        {
            get { return nameField.text; }
            set { nameField.text = value; }
        }

        [SerializeField]
        TMP_Text nameField;

        private void Start()
        {
            _characterPositionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
            _spacesRecord = ObjectFinder.FindSpacesRecord().GetComponent<SpacesRecord>();
        }

        public void GoToSpace()
        {
            var spaceSelectorPosition = spaceSelector.transform.position;
            _characterPositionManager.SlideToTarget(spaceSelectorPosition);
        }
    }
}
