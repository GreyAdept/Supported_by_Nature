using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomEventSystem : MonoBehaviour
{
    public static RandomEventSystem instance;

    [Header("Event Weights")]
    [SerializeField, Range(0f, 100f)] private float catastrophicWeight = 5f;
    [SerializeField, Range(0f, 100f)] private float badWeight = 20f;
    [SerializeField, Range(0f, 100f)] private float neutralWeight = 55f;
    [SerializeField, Range(0f, 100f)] private float goodWeight = 20f;

    [Header("Event Cooldowns")]
    [SerializeField] private int neutralCooldown = 0;
    [SerializeField] private int badCooldown = 1;
    [SerializeField] private int goodCooldown = 2;
    [SerializeField] private int catastrophicCooldown = 3;

    [Header("Event Storage")]
    [SerializeField] private List<WetlandEvent> forcedEvents = new List<WetlandEvent>();
    [SerializeField] private List<WetlandEvent> catastrophicEvents = new List<WetlandEvent>();
    [SerializeField] private List<WetlandEvent> badEvents = new List<WetlandEvent>();
    [SerializeField] private List<WetlandEvent> neutralEvents = new List<WetlandEvent>();
    [SerializeField] private List<WetlandEvent> goodEvents = new List<WetlandEvent>();

    [Header("Other")]
    [SerializeField] private int eventQueLength; //how many events to pregenerate
    public Queue<WetlandEvent> eventQue = new Queue<WetlandEvent>(); //que so we can pregenerate events incase we want to inform player earlier
    private Dictionary<EventCategory, int> categoryCooldowns = new Dictionary<EventCategory, int>();
    private Dictionary<EventCategory, float> currentWeights = new Dictionary<EventCategory, float>(); //weights that have been adjusted based on other factors
    private Dictionary<EventCategory, float> baseWeights = new Dictionary<EventCategory, float>(); //base starting weights
    //ui good, neutral, bad response buttons? send event from button to get event answercategory effects?

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        InitializeWeights();
        InitializeCooldowns();
        for(int i = 0; i < eventQueLength; i++)
        {
            GenerateNewEvent();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ForceNextEvent("kosteikolle_saapuu");
            //GenerateNewEvent();
        }
    }
    private void InitializeWeights()
    {
        baseWeights[EventCategory.Catastrophic] = catastrophicWeight;
        baseWeights[EventCategory.Bad] = badWeight;
        baseWeights[EventCategory.Neutral] = neutralWeight;
        baseWeights[EventCategory.Good] = goodWeight;
    }
    private void InitializeCooldowns()
    {
        categoryCooldowns[EventCategory.Catastrophic] = 0;
        categoryCooldowns[EventCategory.Bad] = 0;
        categoryCooldowns[EventCategory.Neutral] = 0;
        categoryCooldowns[EventCategory.Good] = 0;
    }
    public void ForceNextEvent(string forcedEventId)
    {
        WetlandEvent nextEvent = null;
        foreach(WetlandEvent evt in forcedEvents)
        {
            if(evt.eventId == forcedEventId)
            {
                nextEvent = evt;
                break;
            }
        }
        WetlandEvent[] events = eventQue.ToArray();
        eventQue.Clear();
        eventQue.Enqueue(nextEvent);
        foreach(WetlandEvent evt in events) eventQue.Enqueue(evt);
    }
    public WetlandEvent GetNextEvent()
    {
        if(eventQue.Count == 0)
        {
            GenerateNewEvent();
        }
        WetlandEvent nextEvent = eventQue.Dequeue();
        GenerateNewEvent();
        return nextEvent;
    }
    private void GenerateNewEvent()
    {
        AdjustWeights();
        EventCategory category = SelectCategory();
        WetlandEvent newEvent = SelectRandomEvent(category);
        eventQue.Enqueue(newEvent); //select event from random category and add to que
        UpdateEventsCooldowns(category); //set cooldown for category and reduce others
        Debug.Log($"Generated event type: {category}");
    }
    private EventCategory SelectCategory()
    {
        float totalWeight = 0;
        foreach(var weight in currentWeights)
        {
            totalWeight += weight.Value;
        }

        float randomValue = Random.Range(0, totalWeight);
        float checkWeight = 0;
        foreach(var weight in currentWeights)
        {
            //check if observed category value falls into random number generated
            checkWeight += weight.Value;
            if(randomValue <= checkWeight)
            {
                return weight.Key;
            }
        }
        return EventCategory.Neutral; //backup
    }
    private WetlandEvent SelectRandomEvent(EventCategory category)
    {
        List<WetlandEvent> eventsInCategory = new List<WetlandEvent>();
        switch(category)
        {
            case EventCategory.Catastrophic:
                eventsInCategory = catastrophicEvents;
                break;
            case EventCategory.Bad:
                eventsInCategory = badEvents;
                break;
            case EventCategory.Neutral:
                eventsInCategory = neutralEvents;
                break;
            case EventCategory.Good:
                eventsInCategory = goodEvents;
                break;
        }
        if(eventsInCategory.Count == 0)
        {
            Debug.Log("no events");
        }
        int randomIndex = Random.Range(0, eventsInCategory.Count); //later make it so it can't pick same event from category too many times?
        return eventsInCategory[randomIndex];
    }
    private void UpdateEventsCooldowns(EventCategory category)
    {
        switch(category) //set cooldown to created event's category
        {
            case EventCategory.Catastrophic:
                categoryCooldowns[category] = catastrophicCooldown;
                break;
            case EventCategory.Bad:
                categoryCooldowns[category] = badCooldown;
                break;
            case EventCategory.Neutral:
                categoryCooldowns[category] = neutralCooldown;
                break;
            case EventCategory.Good:
                categoryCooldowns[category] = goodCooldown;
                break;
        }
        foreach(var key in categoryCooldowns.Keys.ToList()) //reduce cooldowns, throws error if keys not turned to list
        {
            if (categoryCooldowns[key] > 0)
            {
                categoryCooldowns[key]--;
            }
        }
    }
    private void AdjustWeights()
    {
        currentWeights[EventCategory.Catastrophic] = baseWeights[EventCategory.Catastrophic];
        currentWeights[EventCategory.Bad] = baseWeights[EventCategory.Bad];
        currentWeights[EventCategory.Neutral] = baseWeights[EventCategory.Neutral];
        currentWeights[EventCategory.Good] = baseWeights[EventCategory.Good];

        //weight 0 if on cooldown so can't be selected
        foreach(var cooldown in categoryCooldowns)
        {
            if(cooldown.Value > 0)
            {
                currentWeights[cooldown.Key] = 0;
            }
        }
        //in case all categories are somehow on cooldown
        bool hasValidCategory = false;
        foreach(var weight in currentWeights)
        {
            if (weight.Value > 0)
            {
                hasValidCategory = true;
            }
        }
        if(!hasValidCategory)
        {
            currentWeights[EventCategory.Neutral] = baseWeights[EventCategory.Neutral];
        }
    }
    public WetlandEvent CheckNextEvent()
    {
        if(eventQue.Count > 0)
        {
            Debug.Log("peeking eventque");
            int j = 0;
            foreach (WetlandEvent e in eventQue)
            {
                Debug.Log($"Event: {e.name} at id: {j}");
                j++;
            }
            Debug.Log($"when peeking we get {eventQue.Peek()}");
            return eventQue.Peek();
        }
        else
        {
            GenerateNewEvent();
            return eventQue.Peek();
        }
    }
}
