using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AddOutlinersToBuilding
{
    public static void AddOutliners(GameObject parent)
    { 

        foreach (Transform child in parent.transform)
        {
			if (child.GetComponent<Outline>() == null)
			{
			    var outline = child.gameObject.AddComponent<Outline>();
				//outline.OutlineMode = Outline.Mode.OutlineAll;
				outline.OutlineColor = Color.yellow;
				outline.OutlineWidth = 0f;
			}
        }
	}
}
