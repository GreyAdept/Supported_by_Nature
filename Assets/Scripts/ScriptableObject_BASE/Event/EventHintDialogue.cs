using UnityEngine;

[CreateAssetMenu(fileName = "EventHint", menuName = "Dialogue/EventHint")]
public class EventHintDialogue : DialogueBase
{
    [Header("Linked Event")]
    public string linkedEventId;

    public bool AppliesToEvent(WetlandEvent eventToCheck)
    {
        if(linkedEventId == eventToCheck.eventId)
        {
            return true;
        }
        return false;
    }
}
