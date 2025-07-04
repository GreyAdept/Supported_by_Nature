using TMPro;
using UnityEngine;
//using static Unity.VisualScripting.Icons;


public class GuideMenu : MonoBehaviour
{
    private LanguageManager language;
    public CanvasGroup mainElements;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI noText;
    public TextMeshProUGUI yesText;


    private void Start()
    {
        language = LanguageManager.Instance;
        //ChangeText();
        this.gameObject.SetActive(false);
    }

    private void ChangeText()
    {
        switch (language.currentLanguage)
        {
            case (Language.FI):
                titleText.text = "Lopeta peli?";
                noText.text = "Ei";
                yesText.text = "Kyllä";
                break;
            case (Language.SW):
                titleText.text = "Avsluta spelet?";
                noText.text = "Nej";
                yesText.text = "Ja";
                break;
            case (Language.EN):
                titleText.text = "Quit game?";
                noText.text = "No";
                yesText.text = "Yes";
                break;
        }
    }


    public void CloseThisMenu()
    {
        mainElements.interactable = true;
        mainElements.alpha = 1;
        this.gameObject.SetActive(false);
        GameMaster.Instance.paused = false;
    }

}
