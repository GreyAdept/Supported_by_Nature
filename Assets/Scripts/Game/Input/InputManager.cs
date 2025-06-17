using UnityEngine;
using UnityEngine.Events;
using Utils;

public class InputManager : SingletonMonoBehaviour<InputManager>
{

    private InputSystem_Actions inputActions;
    public Vector2 pointerPosition;
    public UnityEvent onPointerReleased = new UnityEvent(); 

    public enum PlayerState 
    {   
        paused,
        normal,
        placement
    }

    

    public PlayerState currentPlayerState;
    public ButtonType currentButton; //what button is selected from the UI actions menu
    
   
    void Start()
    {   
        currentPlayerState = PlayerState.normal;

        inputActions = new InputSystem_Actions(); 
        inputActions.Enable();

         
        inputActions.Default.TileAction.canceled += context => //when pointer is pressend and released while in placement mode, do the action
        {
            if (currentPlayerState == PlayerState.placement)
            {
                onPointerReleased.Invoke(); //invoke the created event to fire the action in other class
            }
        };

        inputActions.Default.Point.performed += context => 
        {
            pointerPosition = context.ReadValue<Vector2>();
        };
  
    }

    
}
