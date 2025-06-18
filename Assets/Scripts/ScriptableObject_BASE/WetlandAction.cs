using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WetlandAction", menuName = "WetlandAction")]
public class WetlandAction : ScriptableObject
{
    [Header("Info")]
    public string actionName;
    public string description;
    public Sprite icon;
    [Header("ActionTime")] //building etc.. not yet implemented need discussing
    public bool isInstant;
    public bool canBeCanceled;
    public int turnsToComplete;
    [Header("Cost")]
    public int apCost;
    [Header("Effects")] //temporary, change later
    public List<MetricEffect> instantEffects;
    public List<MetricEffect> ongoingEffects;
    public int ongoingEffectDuration;

    public bool CanBeExecuted(GameState gameState)
    {
        return gameState.currentActionPoints >= apCost;
    }
    public void ExecuteAction(GameState gameState)
    {
        if(instantEffects.Count > 0)
        {
            foreach (MetricEffect effect in instantEffects)
            {
                gameState.QueueMetricEffect(effect);
            }
        }
        if(ongoingEffects.Count > 0)
        {
            gameState.AddOngoingEffect(new OngoingEffect
            {
                effects = ongoingEffects,
                remainingTurns = ongoingEffectDuration
            });
        }
        //gameState.currentActionPoints -= apCost;
    }
}
