using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueDatabase dialogueDB;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image speakerImage;
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TMP_Text taskDescriptionText;
    [SerializeField] private Image taskImage;
    private RandomEventSystem randomEventSystem;
    private TurnManager turnManager;
    [Header("Typewriter Effect")]
    [SerializeField] private bool useEffect = true;
    [SerializeField] private float baseTypingSpeed = 50f;
    [Header("Events")]
    public UnityEvent<string> onTaskAssigned;
    public UnityEvent<string> onTaskCompleted;
    public UnityEvent onTutorialCompleted;

    private int currentTutorialIndex = 0;
    private bool tutorialActive = true;
    private bool hasGivenHintThisTurn = false;
    private string currentTaskId = "";
    private Coroutine typeWriterEffectCoroutine;
    private bool isDialogueRunning = false;
    private string finalText;
    public bool IsDialogueActive => dialoguePanel.activeInHierarchy;

    private void Start()
    {
        randomEventSystem = RandomEventSystem.instance;
        turnManager = TurnManager.Instance;
        turnManager.onTurnChanged.AddListener(OnTurnChanged);
        HideDialogue();
        HideTask();
        if(tutorialActive)
        {
            if(dialogueDB.tutorialSequence.Count > 0)
            {
                ShowNextTutorialDialogue();
            }
            else
            {
                tutorialActive = false;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) InteractWithNPC();
        TemporaryQuestManager();
    }
    private void OnTurnChanged(int turnNumber)
    {
        hasGivenHintThisTurn = false;
        //temp
        HideDialogue();
    }
    private void ShowNextTutorialDialogue()
    {
        if(currentTutorialIndex < dialogueDB.tutorialSequence.Count)
        {
            Debug.Log("we in the wrong place");
            TutorialDialogue tutorial = dialogueDB.tutorialSequence[currentTutorialIndex];
            ShowDialogue(tutorial);
            if(!string.IsNullOrEmpty(tutorial.taskDescription))
            {
                ShowTask(tutorial.taskId, tutorial.taskDescription);
            }
        }
        else
        {
            Debug.Log("we in some other place");
            tutorialActive = false;
            onTutorialCompleted?.Invoke();
        }
    }
    public void CompleteTask(string taskID)
    {
        if(currentTaskId == taskID && currentTutorialIndex < dialogueDB.tutorialSequence.Count)
        {
            TutorialDialogue currentTutorial = dialogueDB.tutorialSequence[currentTutorialIndex];
            currentTutorial.isCompleted = true;
            currentTutorialIndex++;
            onTaskCompleted?.Invoke(taskID);
            currentTaskId = "";
            HideTask();
            if (currentTutorialIndex < dialogueDB.tutorialSequence.Count)
            {
                ShowNextTutorialDialogue();
            }
            else if (currentTutorialIndex >= dialogueDB.tutorialSequence.Count)
            {
                Debug.Log("tutorial not active");
                tutorialActive = false;
                onTutorialCompleted?.Invoke();
            }
        }
    }
    private void ShowTask(string taskId, string taskDescription, Sprite icon = null)
    {
        currentTaskId = taskId;
        taskPanel.SetActive(true);
        taskDescriptionText.text = taskDescription;
        if(icon != null)
        {
            taskImage.sprite = icon;
            taskImage.gameObject.SetActive(true);
        }
        else
        {
            taskImage.gameObject.SetActive(false);
        }
        onTaskAssigned?.Invoke(taskId);
    }
    private void HideTask()
    {
        taskPanel.SetActive(false);
    }
    public void GiveHintForNextEvent()
    {
        if(hasGivenHintThisTurn)
        {
            Debug.Log("has given hint?");
            ShowRandomDialogue();
            return;
        }
        Debug.Log("trying to get next turn event");
        WetlandEvent nextEvent = randomEventSystem.CheckNextEvent();
        Debug.Log(nextEvent.eventCategory.ToString());
        if(nextEvent != null)
        {
            Debug.Log("next event not null");
            EventHintDialogue hint = dialogueDB.GetHintForEvent(nextEvent);
            if(hint != null)
            {
                Debug.Log("hint not null");
                ShowDialogue(hint);
                hasGivenHintThisTurn = true;
            }
        }
    }
    public void ShowRandomDialogue()
    {
        RandomDialogue randomDialogue = dialogueDB.GetRandomDialogue(turnManager.CurrentTurn);
        if(randomDialogue != null)
        {
            ShowDialogue(randomDialogue);
        }
    }
    public void ShowDialogue(DialogueBase dialogue)
    {
        Debug.Log("in the showdialogue script");
        dialoguePanel.SetActive(true);
        speakerNameText.text = dialogue.npcName;
        if(useEffect)
        {
            dialogueText.text = "";
            typeWriterEffectCoroutine = StartCoroutine(TypeWriterEffect(dialogue.dialogueText, dialogue.typeSpeed));
        }
        if(dialogue.npcImage != null)
        {
            speakerImage.sprite = dialogue.npcImage;
            speakerImage.gameObject.SetActive(true);
        }
        else
        {
            speakerImage.gameObject.SetActive(false);
        }
        isDialogueRunning = true;
    }
    private IEnumerator TypeWriterEffect(string text, float speed)
    {
        finalText = text;
        dialogueText.text = "";
        float typingSpeed = speed * baseTypingSpeed;
        float timePerChar = 1f/typingSpeed;
        for(int i = 0; i< text.Length; i++)
        {
            dialogueText.text += text[i];
            if(i < text.Length - 1)
            {
                yield return new WaitForSeconds(timePerChar);
            }
        }
        typeWriterEffectCoroutine = null;
    }
    public void HideDialogue()
    {
        if(isDialogueRunning && typeWriterEffectCoroutine != null)
        {
            StopCoroutine(typeWriterEffectCoroutine);
            isDialogueRunning = false;
        }
        isDialogueRunning = false;
        dialoguePanel.SetActive(false); 
    }
    public void InteractWithNPC()
    {
        if(isDialogueRunning)
        {
            Debug.Log("dialoguerunning");
            if(typeWriterEffectCoroutine != null)
            {
                Debug.Log("typewriter not null");
                StopCoroutine(typeWriterEffectCoroutine);
                dialogueText.text = finalText;
                typeWriterEffectCoroutine = null;
            }
            else
            {
                Debug.Log("trying to hide");
                HideDialogue();
            }
            return;
        }
        if(tutorialActive)
        {
            Debug.Log("trying to show tutorial");
            ShowNextTutorialDialogue();
        }
        else if(!hasGivenHintThisTurn)
        {
            Debug.Log("trying to give hint");
            GiveHintForNextEvent();
        }
        else
        {
            Debug.Log("trying to show random dialogue");
            ShowRandomDialogue();
        }
    }
    private void TemporaryQuestManager()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) CompleteTask("task_one");
        if (Input.GetKeyDown(KeyCode.Alpha2)) CompleteTask("task_two");
    }
}
