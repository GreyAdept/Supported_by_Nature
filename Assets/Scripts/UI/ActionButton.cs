using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    //manager classes
    private tileManager tm;
    private InputManager inputManager;
    //
    
    [SerializeField]private bool selected = false; 

    private Vector3 originalPosition;
    private RectTransform rect;

    public ButtonType buttonType; //type of action the button performs, currently planting or cutting

    public static event System.Action<ButtonType> OnButtonSelectionChanged;
    public static event System.Action<InputManager.PlayerState> OnPlayerStateChanged;

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
    
    public void OnPointerDown(PointerEventData pointerEventData) //on pointer down, set the button state as selected and send the button type of input manager
    {   
        selected = true;
        tm.toolBeingUsed = true;
        
        OnButtonSelectionChanged?.Invoke(buttonType);
        OnPlayerStateChanged?.Invoke(InputManager.PlayerState.placement);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        selected = false;
        tm.toolBeingUsed = false;
        
        rect.anchoredPosition = originalPosition; //reset the button to its original spot 

        OnPlayerStateChanged?.Invoke(InputManager.PlayerState.normal);

    }
    
  



}
