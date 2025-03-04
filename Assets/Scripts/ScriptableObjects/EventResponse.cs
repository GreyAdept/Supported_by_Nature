using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EventResponse", menuName = "EventResponse")]
public class EventResponse : ScriptableObject
{
    public int linkedEventId;
    public AnswerCategory answerCategory;
    public string responseText;
    public string outcomeText;
    public List<MetricEffect> effects;
}
