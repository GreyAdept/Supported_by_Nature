using UnityEngine;
using TMPro;
public class EventPanelUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject eventPopupPanel;
    [Header("Text Boxes")]
    [SerializeField] private TMP_Text eventNameText;
    [SerializeField] private TMP_Text eventDescriptionText;
    [SerializeField] private TMP_Text goodResponseButtonText;
    [SerializeField] private TMP_Text neutralResponseButtonText;
    [SerializeField] private TMP_Text badResponseButtonText;
    private WetlandEvent currentEvent;
    private TurnManager turnManager;
    //quick shitty script for testing etc.
    private void Start()
    {
        turnManager = TurnManager.Instance;
    }
    public void GetNewEvent()
    {
        currentEvent = turnManager.gameState.CurrentEvent;
        Debug.Log(currentEvent.name);
        SetupEventUI();
    }
    private void SetupEventUI()
    {
        eventNameText.text = currentEvent.eventName;
        /*eventDescriptionText.text = currentEvent.eventDescription;
        goodResponseButtonText.text = currentEvent.GetResponseFromAnswer(AnswerCategory.Good).responseText;
        neutralResponseButtonText.text = currentEvent.GetResponseFromAnswer(AnswerCategory.Neutral).responseText;
        badResponseButtonText.text = currentEvent.GetResponseFromAnswer(AnswerCategory.Bad).responseText;*/
        ShowEventUI();
    }
    public void ShowEventUI()
    {
        eventPopupPanel.SetActive(true);
    }
    public void HideEventUI()
    {
        eventPopupPanel.SetActive(false);
    }
    //method to show outcome text after click event
}
