using System;
using TMPro;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public tileAction action;
    private tileManager tm;
    public TextMeshProUGUI buttonText;

    void Start()
    {
        tm = tileManager.Instance;
        if (action != null)
        {
            buttonText.text = action.actionName;
        }
        else
        {
            buttonText.text = "Placeholder Action";
        }
        
    }

    public void ClickButton()
    {
        if (tm.selectedTile != null && action != null)
        {
            action.affectTile(tm.selectedTile);
        }
        
    }
    
}
