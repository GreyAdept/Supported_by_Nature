using TMPro;
using UnityEngine;

public class ButtonCloseGame : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private LanguageManager language;

    public CanvasGroup mainElements;

    public GameObject closeGameMenu;

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
                buttonText.text = "Lopeta peli";
                break;
            case (Language.SW):
                buttonText.text = "Avsluta spelet";
                break;
            case (Language.EN):
                buttonText.text = "Quit game";
                break;
        }
    }

    public void ShowCloseGameMenu()
    {
        mainElements.alpha = 0;
        mainElements.interactable = false;
        closeGameMenu.SetActive(true);
        GameMaster.Instance.paused = true;
    }
}
