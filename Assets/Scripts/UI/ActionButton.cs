using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class ActionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public tileAction action;
    private tileManager tm;
    public TextMeshProUGUI buttonText;
    private bool selected = false;
    private Vector3 originalPosition;
    private mouseRaycaster mouseRaycaster;

    void Start()
    {
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
        tm = tileManager.Instance;
        mouseRaycaster = tm.gameObject.GetComponent<mouseRaycaster>();
        /*
        if (action != null)
        {
            buttonText.text = action.actionName;
        }
        else
        {
            buttonText.text = "Placeholder Action";
        }
        */
    }

    void Update()
    {
        if (selected)
        {
            Vector2 inputPos;
            if (Touchscreen.current != null && mouseRaycaster.isTouching)
            {
                inputPos = mouseRaycaster.touchPosition;
            }
            else
            {
                inputPos = Mouse.current.position.ReadValue();
            }
            
            
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exit");
    }
    
    public void OnPointerClick (PointerEventData eventData)
    {   
       if (tm.selectedTile != null)
        {
            action.affectTile(tm.selectedTile);
            Debug.Log("clicked!" + TurnManager.Instance.gameState.currentActionPoints);
        }
        
    }
    
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        selected = true;
        tm.toolBeingUsed = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if(selected && tm.selectedTile != null)
        {
            action.affectTile(tm.selectedTile);
        }
        selected = false;
        tm.toolBeingUsed = false;
        GetComponent<RectTransform>().anchoredPosition = originalPosition;
    }
    
    /*
    public void ClickButton()
    {
        if (tm.selectedTile != null && action != null)
        {
            action.affectTile(tm.selectedTile);
        }
        
    }
    */
    
}
