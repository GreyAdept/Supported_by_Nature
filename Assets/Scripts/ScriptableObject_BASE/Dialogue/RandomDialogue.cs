using UnityEngine;

[CreateAssetMenu(fileName = "Random", menuName = "Dialogue/Random")]
public class RandomDialogue : DialogueBase
{
    [Header("Context")]
    public bool isJoke = false;
    public bool isEducational = false;
    [Header("Requirements")]
    public bool hasTurnRequirement = false;
    public int minTurn;
    public int maxTurn;
    public bool IsValidCurrently(int currentTurn) // add more? gamestate, metrics etc.
    {
        if(hasTurnRequirement && currentTurn < minTurn || currentTurn > maxTurn)
        {
            return false;
        }
        return true;
    }
}
