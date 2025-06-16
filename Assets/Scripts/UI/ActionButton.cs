using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    private tileManager tm;
    private InputManager inputManager;
    [SerializeField]private bool selected = false;
    private Vector3 originalPosition;
    private RectTransform rect;
    public ButtonType buttonType;

    void Start()
    {
        inputManager = InputManager.Instance;
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;
        tm = tileManager.Instance;
       
       
    }

    void Update()
    {
        if (selected)
        {
           transform.position = inputManager.pointerPosition;
        }
    }
    
    public void OnPointerDown(PointerEventData pointerEventData)
    {   
        selected = true;
        tm.toolBeingUsed = true;
        inputManager.currentPlayerState = InputManager.PlayerState.placement;
        inputManager.currentButton = buttonType;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        selected = false;
        tm.toolBeingUsed = false;
        inputManager.currentPlayerState = InputManager.PlayerState.normal;
        rect.anchoredPosition = originalPosition;
    }

    


}
