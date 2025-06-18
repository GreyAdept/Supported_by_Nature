using UnityEngine;
using Utils;
using System;
using Action = System.Action;

//"master" class to provide static C# events to all other classes
public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
    
    #region Player Control Events
    public static event Action<ButtonType> OnToolButtonChanged;
    #endregion                                                                                      
                                                                                                                            
    #region Game State Events
    public static event Action<PlayerState> OnPlayerStateChanged;
    public static event Action<int> OnActionPointsChanged;
    
    public static event Action OnMetricsUpdated;
    #endregion

    #region Game Tile Events
    public static event Action<GameObject> OnPlantPlaced;
    #endregion

    #region UI Events
    public static event Action OnNPCInterract;
    public static event Action OnMenuClosed;
    #endregion

    public GameState gameState { get; private set; }
    public InputManager inputManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        gameState = GetComponent<GameState>();
        inputManager = GetComponent<InputManager>();
    }
}
