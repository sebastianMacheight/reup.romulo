using UnityEngine;
using TMPro;
using ReupVirtualTwin.helpers;
using UnityEngine.Events;

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
            _characterPositionManager.MakeKinematic();
            var spaceSelectorPosition = spaceSelector.transform.position;
            var endMovementEvent = new UnityEvent();
            endMovementEvent.AddListener(GetToSpaceHandler);
            _characterPositionManager.SlideToTarget(spaceSelectorPosition,endMovementEvent);
        }
        private void GetToSpaceHandler()
        {
            Debug.Log("movement finished");
            _characterPositionManager.UndoKinematic();
        }
    }
}
