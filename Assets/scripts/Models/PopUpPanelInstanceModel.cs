using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ReUpVirtualTwin.Models;

public class PopUpPanelInstanceModel : PanelInstanceModel
{
    public string title;

    public PopUpPanelInstanceModel(GameObject instance, string titleText) 
    {
		panelId = PanelsEnum.PopUpPanel;
        panelInstance = instance;
        title = titleText;
        TMP_Text panelTitle = panelInstance.GetComponentInChildren<TMP_Text>();
        panelTitle.text = title;
    }
}
