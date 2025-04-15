using UnityEngine;

[CreateAssetMenu(fileName = "WetlandEvent", menuName = "WetlandEvent")]
public class WetlandEvent : ScriptableObject
{
    public string eventId;
    public string eventName;
    [TextArea(3,6)] public string eventDescription;
    public LocalizedText eventNameLocalized;
    public LocalizedText eventDescriptionLocalized;
    public Sprite eventIcon;
    public EventCategory eventCategory;

    public EventResponse goodResponse;
    public EventResponse neutralResponse;
    public EventResponse badResponse;

    public EventResponse GetResponseFromAnswer(AnswerCategory answerCategory)
    {
        switch(answerCategory)
        {
            case AnswerCategory.Good:
                return goodResponse;
            case AnswerCategory.Neutral:
                return neutralResponse;
            case AnswerCategory.Bad:
                return badResponse;
            default:
                return null;
        }
    }
    //event metric modifiers..
}
