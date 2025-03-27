using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EventResponse", menuName = "EventResponse")]
public class EventResponse : ScriptableObject
{
    public string linkedEventId;
    public AnswerCategory answerCategory;
    [TextArea(3, 6)] public string responseText;
    [TextArea(3, 6)] public string outcomeText;
    public List<MetricEffect> effects;
}
