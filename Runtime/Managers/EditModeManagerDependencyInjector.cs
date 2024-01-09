using ReupVirtualTwin.behaviourInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManagerDependencyInjector : MonoBehaviour
    {
        EditModeManager _editModeManager;
        private void Awake()
        {
            _editModeManager = GetComponent<EditModeManager>();
            IWebMessagesSender webMessageSender = GetComponent<IWebMessagesSender>();
            if (webMessageSender == null )
            {
                throw new System.Exception("WebMessageSender not found");
            }
            _editModeManager.webMessageSender = webMessageSender;
        }
    }
}
