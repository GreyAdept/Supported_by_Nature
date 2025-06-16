using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EventResponse", menuName = "EventResponse")]
public class EventResponse : ScriptableObject
{
    public string linkedEventId;
    public AnswerCategory answerCategory;
    [TextArea(1, 3)] public string responseText;
    public LocalizedText responseTextLocalized;
    [TextArea(1, 3)] public string outcomeText;
    public LocalizedText outcomeTextLocalized;
    public List<MetricEffect> effects;
}
