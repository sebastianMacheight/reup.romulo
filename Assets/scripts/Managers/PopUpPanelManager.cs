using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopUpPanelManager : MonoBehaviour
{
    private PopUpPanelInstanceModel _currentPopPanel = null;
    private bool _prevPopUpPanel = false;
    public DragManager dragManager;
    public PanelManager panelManager;
    private string panelId = PanelsEnum.PopUpPanel;
    private InputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = new InputProvider();
    }

    private void OnEnable()
    {
        _inputProvider.selectCanceled += OnSelect;
    }

    private void OnDisable()
    {
        _inputProvider.selectCanceled -= OnSelect;
    }

    private void Update()
    {
        if (_currentPopPanel != null) {
            _prevPopUpPanel = true;
		}
        else {
            _prevPopUpPanel = false;
		}
    }

    public void ShowPopUpPanel(GameObject obj)
    { 
        // If there is a current pop up panel, hide that one instead
        if (_currentPopPanel != null)
        {
		    HidePopUpPanel();
            return;
		}
		var panelInstance = panelManager.ShowPanel(panelId);
		_currentPopPanel = new PopUpPanelInstanceModel(panelInstance, obj.transform.name);
        _currentPopPanel.panelInstance.transform.position = _inputProvider.PointerInput();
    }

    public void HidePopUpPanel()
    { 
		panelManager.HidePanel(panelId);
		_currentPopPanel = null;
    }

    protected void OnSelect(InputAction.CallbackContext obj)
    {
        if (_currentPopPanel != null && !dragManager.prevDragging && _prevPopUpPanel)
        {
            HidePopUpPanel();
		}
    }
}
