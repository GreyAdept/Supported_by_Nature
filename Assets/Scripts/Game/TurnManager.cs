using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public WarningMessagesUI warningMessages;
    public MilestoneHandler milestoneHandler;
    public static TurnManager Instance;
    public GameState gameState;
    public int CurrentTurn => currentTurn;
    private int currentTurn = 1;

    public UnityEvent<int> onTurnChanged;
    public static System.Action OnTurnChanged;
    public UnityEvent<int> onActionPointsChanged;
    public UnityEvent<Dictionary<MetricType, float>> onMetricsUpdated;
    [SerializeField] private Button endTurnButton;
    //and more...


    //create global instance so we can access this easily
    private void Awake()
    {   
        milestoneHandler = GetComponent<MilestoneHandler>();
        
        if(Instance == null && Instance != this)
        {
            Instance = this;
            gameState = GetComponent<GameState>();
        }
        else
        {
            Debug.Log("duplicate turnmanager");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(DelayedInitialize());
    }
    //make sure initial event subscriptions go through before sending them
    private IEnumerator DelayedInitialize()
    {
        yield return null;
        Initialize();
    }
    //invoke events at game start to update all base values etc.
    private void Initialize()
    {   
        OnTurnChanged?.Invoke();
        onTurnChanged?.Invoke(currentTurn);
        onActionPointsChanged?.Invoke(gameState.currentActionPoints);
        onMetricsUpdated?.Invoke(gameState.metrics);
    }
    
    //handle end turn logic and trigger events to update UI etc. for next turn 
    public void EndTurn()
    {
        gameState.EndTurn();
        currentTurn++;
        onTurnChanged?.Invoke(currentTurn);
        OnTurnChanged?.Invoke();
        onActionPointsChanged?.Invoke(gameState.currentActionPoints);
        onMetricsUpdated?.Invoke(gameState.metrics);
        ToggleEndTurnButton(false);
    }
    public void ToggleEndTurnButton(bool state)
    {
        if(endTurnButton != null)
        {
            endTurnButton.interactable = state;
        }
    }
    
}
