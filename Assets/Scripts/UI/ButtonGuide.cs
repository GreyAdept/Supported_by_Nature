using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonGuide : APopUp
{
    public TextMeshProUGUI buttonText;

    private LanguageManager language;

    public CanvasGroup mainElements;

    public GameObject guideMenu;

    private void Start()
    {
        language = LanguageManager.Instance;
        ChangeText();
    }

    private void ChangeText()
    {
        switch (language.currentLanguage)
        {
            case (Language.FI):
                buttonText.text = "Ohjeet";
                break;
            case (Language.SW):
                buttonText.text = "Instruktioner";
                break;
            case (Language.EN):
                buttonText.text = "Instructions";
                break;
        }
        
    }

    public void ShowGuideMenu()
    {
        //mainElements.alpha = 0;
        mainElements.interactable = false;
        guideMenu.SetActive(true);
        GameMaster.Instance.paused = true;
    }

    public void CloseGuideMenu()
    {
        //mainElements.alpha = 1;
        mainElements.interactable = true;
        guideMenu.SetActive(false);
        GameMaster.Instance.paused = false;
    }

    /*
    public override void OpenPopUp()
    {   
        base.OpenPopUp();
    }

    public override void ClosePopUp()
    {
        base.ClosePopUp();
    }
    */
}

