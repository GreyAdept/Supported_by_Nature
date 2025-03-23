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

    void Start()
    {
        originalPosition = GetComponent<RectTransform>().position;
        tm = tileManager.Instance;
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
            var mousePos = Mouse.current.position.ReadValue();
            transform.position = mousePos;
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
        Debug.Log("clicked");
    }
    
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        selected = true;
        tm.toolBeingUsed = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        selected = false;
        tm.toolBeingUsed = false;
        GetComponent<RectTransform>().position = originalPosition;
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
