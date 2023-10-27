using UnityEngine;
using TMPro;

namespace ReupVirtualTwin.characterMovement
{
    public class SpaceButtonInstance : MonoBehaviour
    {
        public SpaceJumpPoint spaceSelector;

        public string spaceName
        {
            get { return nameField.text; }
            set { nameField.text = value; }
        }

        [SerializeField]
        TMP_Text nameField;
    }
}
