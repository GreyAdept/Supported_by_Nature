using TMPro;
using UnityEngine;

public class PopUp_Tutorial_Milestones : MonoBehaviour
{

    private TextMeshProUGUI popUpText;

    void Awake()
    {
        MilestoneHandler.onFirstMilestoneTriggered += OpenWithDelay;
        MilestoneHandler.onTutorialDone += ClosePopUp;
    }

    private void OnDisable()
    {
        MilestoneHandler.onFirstMilestoneTriggered -= OpenWithDelay;
        MilestoneHandler.onTutorialDone -= ClosePopUp;
    }


    void Start()
    {
        ClosePopUp();
        popUpText = GetComponent<TextMeshProUGUI>();

        switch (LanguageManager.Instance.currentLanguage)
        {
            case Language.FI:
                popUpText.text = "Vihje: voit nyt avata ensimmäisen virstanpylvään käyttämällä energiaa!";
                break;
            case Language.SW:
                popUpText.text = "Du kan nu låsa upp den första milstolpen med energi!";
                break;
            case Language.EN:
                popUpText.text = "You can now unlock the first milestone with your energy!";
                break;
        }
    }

    public void ClosePopUp()
    {
        this.gameObject.SetActive(false);
        GameMaster.Instance.paused = false;
    }
    public void OpenPopUp()
    {
        GameMaster.Instance.paused = true;
        this.gameObject.SetActive(true);
        Invoke("ClosePopUp", 5f);
    }

    public void OpenWithDelay()
    {
        Invoke("OpenPopUp", 2f);
    }
}
