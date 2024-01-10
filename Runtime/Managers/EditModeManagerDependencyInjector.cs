using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.managerInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManagerDependencyInjector : MonoBehaviour
    {
        EditModeManager _editModeManager;
        [SerializeField]
        GameObject editionMediator;
        private void Awake()
        {
            _editModeManager = GetComponent<EditModeManager>();
            _editModeManager.mediator = editionMediator.GetComponent<IMediator>();
        }
    }
}
