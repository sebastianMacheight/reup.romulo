using UnityEngine;

using ReupVirtualTwin.inputs;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;

namespace ReupVirtualTwin.behaviours
{
    public class ChangeHeight : MonoBehaviour
    {
        private InputProvider _inputProvider;
        private float CHANGE_SPEED_M_PER_SECOND = 1;
        private IMediator _mediator;
        public IMediator mediator { set => _mediator = value; }

        private void Awake()
        {
            _inputProvider = new InputProvider();
        }

        private void Update()
        {
            float changeHeightInput = _inputProvider.ChangeHeightInput();
            if (changeHeightInput != 0)
            {
                float deltaHeight = changeHeightInput * CHANGE_SPEED_M_PER_SECOND * Time.deltaTime;
                _mediator.Notify(ReupEvent.addToCharacterHeight, deltaHeight);
            }
        }
    }
}
