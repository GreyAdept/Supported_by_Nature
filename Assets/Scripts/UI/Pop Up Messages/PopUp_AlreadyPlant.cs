using TMPro;
using UnityEngine;

public class PopUp_AlreadyPlant : MonoBehaviour
{

    private TextMeshProUGUI popUpText;

    void Awake()
    {
        PlacementSystem.onExistingPlantWarning += () => OpenPopUp();
    }

    void Start()
    {
        ClosePopUp();
        popUpText = GetComponent<TextMeshProUGUI>();

        switch (LanguageManager.Instance.currentLanguage)
        {
            case Language.FI:
                popUpText.text = "Tässä on jo kasvi!";
                break;
            case Language.SW:
                popUpText.text = "Det finns redan en växt!";
                break;
            case Language.EN:
                popUpText.text = "There is already a plant!";
                break;
        }
    }

    public void ClosePopUp()
    {
        this.gameObject.SetActive(false);
    }
    public void OpenPopUp()
    {
        Debug.Log("Event received", this);
        this.gameObject.SetActive(true);
        Invoke("ClosePopUp", 1.5f);
    }
}
