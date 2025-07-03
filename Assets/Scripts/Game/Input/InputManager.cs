using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

public class InputManager : SingletonMonoBehaviour<InputManager>
{

    private InputSystem_Actions inputActions;
    public Vector2 pointerPosition;

    public static event System.Action OnPointerReleased;

    
    public enum PlayerState 
    {   
        paused,
        normal,
        placement
    }

    public PlayerState currentPlayerState;
    public ButtonType currentButton; //what button is selected from the UI actions menu

    public GameObject placementIndicator;
    
   
    void Start()
    {   
        currentPlayerState = PlayerState.normal;

        ActionButton.OnButtonSelectionChanged += ChangeButton;

        ActionButton.OnPlayerStateChanged += UpdatePlayerState;

        inputActions = new InputSystem_Actions(); 
        inputActions.Enable();

        inputActions.Default.TileAction.canceled += PointerReleased;

        inputActions.Default.Point.performed += UpdateMouseValue;
        
    }

    private void PointerReleased(InputAction.CallbackContext ctx)
    {
        if (currentPlayerState == PlayerState.placement)
        {
            OnPointerReleased?.Invoke();
        }
    }


    private void UpdatePlayerState(PlayerState ctx)
    {
        currentPlayerState = ctx;
    }


    private void UpdateMouseValue(InputAction.CallbackContext ctx)
    {
        pointerPosition = ctx.ReadValue<Vector2>();
    }

    private void ChangeButton(ButtonType ctx)
    {
        currentButton = ctx;
    }


    private void OnDisable()
    {
        inputActions.Default.Point.performed -= UpdateMouseValue;
        ActionButton.OnButtonSelectionChanged -= ChangeButton;
        ActionButton.OnPlayerStateChanged -= UpdatePlayerState;
        inputActions.Default.TileAction.canceled -= PointerReleased;
        inputActions.Disable();
    }
}
