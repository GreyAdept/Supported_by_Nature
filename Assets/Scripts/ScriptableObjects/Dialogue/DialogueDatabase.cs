using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "DialogueDatabase")]
public class DialogueDatabase : ScriptableObject
{
    [Header("TutorialSequence")]
    public List<TutorialDialogue> tutorialSequence = new List<TutorialDialogue>();

    [Header("Event Hints")]
    public List<EventHintDialogue> catastrophicEventHints = new List<EventHintDialogue>();
    public List<EventHintDialogue> badEventHints = new List<EventHintDialogue>();
    public List<EventHintDialogue> neutralEventHints = new List<EventHintDialogue>();
    public List<EventHintDialogue> goodEventHints = new List<EventHintDialogue>();

    [Header("Random Dialogues")]
    public List<RandomDialogue> randomDialogues = new List<RandomDialogue>();
    public List<RandomDialogue> jokeDialogues = new List<RandomDialogue>();
    public List<RandomDialogue> educationalDialogues = new List<RandomDialogue>();

    public EventHintDialogue GetHintForEvent(WetlandEvent upcomingEvent)
    {
        EventCategory upcomingEventCategory = upcomingEvent.eventCategory;
        Debug.Log($"upcoming event category: {upcomingEventCategory.ToString()}");
        List<EventHintDialogue> categoryHints = null;
        switch(upcomingEventCategory)
        {
            case EventCategory.Catastrophic:
                categoryHints = catastrophicEventHints;
                break;
            case EventCategory.Bad:
                categoryHints = badEventHints;
                break;
            case EventCategory.Neutral:
                categoryHints = neutralEventHints;
                break;
            case EventCategory.Good:
                categoryHints = goodEventHints;
                break;
        }
        Debug.Log(categoryHints[0].dialogueText);
        if(categoryHints == null)
        {
            return null;
        }
        EventHintDialogue specificMatch = null;
        foreach(EventHintDialogue dialogue in categoryHints)
        {
            if(dialogue.linkedEventId == upcomingEvent.eventId)
            {
                specificMatch = dialogue;
            }
        }
        return specificMatch;
    }
    public RandomDialogue GetRandomDialogue(int currentTurn)
    {
        List<RandomDialogue> candidates;
        float randomValue = Random.value;
        if(randomValue < 0.3f)
        {
            candidates = educationalDialogues;
        }
        else if(randomValue > 0.3f && randomValue < 0.6f)
        {
            candidates = randomDialogues;
        }
        else
        {
            candidates = jokeDialogues;
        }
        if(candidates != null)
        {
            return candidates[Random.Range(0, candidates.Count)];
        }
        else
        {
            return null;
        }

    }
    public void ResetTutorialState()
    {
        foreach(var tutorial in tutorialSequence)
        {
            tutorial.ResetTutorialState();
        }
    }
}
