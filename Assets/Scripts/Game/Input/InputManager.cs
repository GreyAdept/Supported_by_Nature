using UnityEngine;
using UnityEngine.Events;
using Utils;

public class InputManager : MonoBehaviour
{

    private InputSystem_Actions inputActions;
    public Vector2 pointerPosition;

    public static event System.Action OnPointerReleased;
    
    public PlayerState currentPlayerState;
    public ButtonType currentButton; //what button is selected from the UI actions menu
    
   
    void Start()
    {   
        currentPlayerState = PlayerState.normal;

        /*
        ActionButton.OnButtonSelectionChanged += (ButtonType ctx) => currentButton = ctx;

        ActionButton.OnPlayerStateChanged += (PlayerState ctx) => currentPlayerState = ctx;
        */

        GameMaster.OnToolButtonChanged += (ButtonType context) => currentButton = context;
        GameMaster.OnPlayerStateChanged += (PlayerState context) => currentPlayerState = context;


        inputActions = new InputSystem_Actions(); 
        inputActions.Enable();

         
        inputActions.Default.TileAction.canceled += context => 
        {
            if (currentPlayerState == PlayerState.placement)
            {
                OnPointerReleased?.Invoke(); 
            }
        };

        inputActions.Default.Point.performed += context => 
        {
            pointerPosition = context.ReadValue<Vector2>();
        };
  
    }

    
}
