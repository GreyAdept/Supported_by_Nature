using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class EventPanelUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject eventPopupPanel;
    [SerializeField] private GameObject outcomePopupPanel;
    [Header("Text Boxes")]
    [SerializeField] private TMP_Text eventNameText;
    [SerializeField] private TMP_Text eventDescriptionText;
    [SerializeField] private TMP_Text goodResponseButtonText;
    [SerializeField] private TMP_Text neutralResponseButtonText;
    [SerializeField] private TMP_Text badResponseButtonText;
    [SerializeField] private TMP_Text outcomeText;
    private WetlandEvent currentEvent;
    private TurnManager turnManager;

    [SerializeField] private Button goodResponseButton;
    [SerializeField] private Button neutralResponseButton;
    [SerializeField] private Button badResponseButton;
    [SerializeField] private Button okButton;
    [SerializeField] private Button closeOutcomePanelButton;
    /*[SerializeField] private Color defaultButtonColor;
    [SerializeField] private Color selectedButtonColor;*/
    [SerializeField] private Sprite defaultButtonSprite;
    [SerializeField] private Sprite selectedButtonSprite;
    private AnswerCategory? selectedAnswer = null;


    private void Start()
    {
        turnManager = TurnManager.Instance;
        goodResponseButton.onClick.AddListener(() => SelectResponse(AnswerCategory.Good));
        neutralResponseButton.onClick.AddListener(() => SelectResponse(AnswerCategory.Neutral));
        badResponseButton.onClick.AddListener(() => SelectResponse(AnswerCategory.Bad));
        okButton.onClick.AddListener(ConfirmSelection);
        closeOutcomePanelButton.onClick.AddListener(CloseOutcomePanel);
        okButton.interactable = false;
    }
    public void GetNewEvent()
    {
        currentEvent = turnManager.gameState.CurrentEvent;
        Debug.Log(currentEvent.name);
        SetupEventUI();
        ResetSelectionState();
    }
    private void SetupEventUI()
    {
        /*eventNameText.text = currentEvent.eventName ?? string.Empty;
        eventDescriptionText.text = currentEvent.eventDescription ?? string.Empty;
        goodResponseButtonText.text = currentEvent.GetResponseFromAnswer(AnswerCategory.Good).responseText ?? string.Empty;
        neutralResponseButtonText.text = currentEvent.GetResponseFromAnswer(AnswerCategory.Neutral).responseText ?? string.Empty;
        badResponseButtonText.text = currentEvent.GetResponseFromAnswer(AnswerCategory.Bad).responseText ?? string.Empty;*/
        eventNameText.text = currentEvent.eventName ?? string.Empty;
        eventDescriptionText.text = currentEvent.eventDescription ?? string.Empty;
        var goodResponse = currentEvent.GetResponseFromAnswer(AnswerCategory.Good);
        goodResponseButtonText.text = goodResponse != null ? (goodResponse.responseText ?? string.Empty) : string.Empty;
        var neutralResponse = currentEvent.GetResponseFromAnswer(AnswerCategory.Neutral);
        neutralResponseButtonText.text = neutralResponse != null ? (neutralResponse.responseText ?? string.Empty) : string.Empty;
        var badResponse = currentEvent.GetResponseFromAnswer(AnswerCategory.Bad);
        badResponseButtonText.text = badResponse != null ? (badResponse.responseText ?? string.Empty) : string.Empty;
        ShowEventUI();
    }
    public void ShowEventUI()
    {
        eventPopupPanel.transform.localScale = Vector3.zero;
        eventPopupPanel.SetActive(true);
        eventPopupPanel.transform.DOScale(Vector3.one, 0.4f).SetUpdate(true);
    }
    public void HideEventUI()
    {
        eventPopupPanel.transform.DOScale(Vector3.zero, 0.2f).SetUpdate(true).OnComplete(() => eventPopupPanel.SetActive(false));
        //eventPopupPanel.SetActive(false);
    }
    private void ResetSelectionState()
    {
        selectedAnswer = null;
        ResetButtonColors();
        okButton.interactable = false;
    }
    private void ResetButtonColors()
    {
        goodResponseButton.GetComponent<Image>().sprite = defaultButtonSprite;
        neutralResponseButton.GetComponent<Image>().sprite = defaultButtonSprite;
        badResponseButton.GetComponent<Image>().sprite = defaultButtonSprite;
    }
    private void SelectResponse(AnswerCategory answer)
    {
        ResetButtonColors();
        selectedAnswer = answer;
        Button selectedButton = GetButtonForAnswer(answer);
        selectedButton.GetComponent<Image>().sprite = selectedButtonSprite;
        okButton.interactable = true;
    }
    private Button GetButtonForAnswer(AnswerCategory answer)
    {
        switch(answer)
        {
            case AnswerCategory.Good:
                return goodResponseButton;
            case AnswerCategory.Neutral:
                return neutralResponseButton;
            case AnswerCategory.Bad:
                return badResponseButton;
            default: return null;
        }
    }
    private void ConfirmSelection()
    {
        if(selectedAnswer.HasValue)
        {
            turnManager.gameState.HandleRandomEvent(selectedAnswer.Value);
            DisplayEventOutcome(selectedAnswer.Value);
            ResetSelectionState();
        }
    }
    private void DisplayEventOutcome(AnswerCategory answer)
    {
        EventResponse selectedResponse = currentEvent.GetResponseFromAnswer(answer);
        outcomePopupPanel.transform.localScale = Vector3.zero;
        outcomePopupPanel.SetActive(true);
        outcomeText.text = selectedResponse.outcomeText;
        outcomePopupPanel.transform.DOScale(Vector3.one, 0.4f).SetUpdate(true);
    }
    private void CloseOutcomePanel()
    {
        outcomePopupPanel.transform.DOScale(Vector3.zero, 0.2f).SetUpdate(true).OnComplete(()=>outcomePopupPanel.SetActive(false));
    }
}
