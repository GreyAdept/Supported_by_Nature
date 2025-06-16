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
    public ButtonType currentButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        currentPlayerState = PlayerState.normal;

        inputActions = new InputSystem_Actions();
        inputActions.Enable();

         
        inputActions.Default.TileAction.canceled += context =>
        {
            if (currentPlayerState == PlayerState.placement)
            {
                onPointerReleased.Invoke();
            }
        };

        inputActions.Default.Point.performed += context =>
        {
            pointerPosition = context.ReadValue<Vector2>();
        };
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
