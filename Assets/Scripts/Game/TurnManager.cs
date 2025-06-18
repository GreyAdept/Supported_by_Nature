using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public WarningMessagesUI warningMessages;
    public MilestoneHandler milestoneHandler; 

    public UnityEvent<Dictionary<MetricType, float>> onMetricsUpdated;
    public static event Action OnTurnChanged;
    
  
    private void Start()
    {
        StartCoroutine(DelayedInitialize());
    }
    
    private IEnumerator DelayedInitialize()
    {
        yield return null;
        Initialize();
    }

    private void Initialize()
    {   
      
    }

    public void EndTurn()
    {
        OnTurnChanged?.Invoke();
    }

    /*
    //handle end turn logic and trigger events to update UI etc. for next turn 
    public void EndTurnOld()
    {
        //gameState.EndTurn();
        currentTurn++;
        onTurnChanged?.Invoke(currentTurn);
        //onActionPointsChanged?.Invoke(gameState.currentActionPoints);
        //onMetricsUpdated?.Invoke(gameState.metrics);
        ToggleEndTurnButton(false);
    }
    */


    public void ToggleEndTurnButton(bool state)
    {
        //if(endTurnButton != null)
        //{
        //    endTurnButton.interactable = state;
        //}
    }
    
}
