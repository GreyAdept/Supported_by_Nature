using TMPro;
using UnityEngine;

public class PopUp_Overgrown : MonoBehaviour
{

    private TextMeshProUGUI popUpText;

    void Awake()
    {
        PlacementSystem.onOvergrownWarning += OpenPopUp;
    }

    private void OnDisable()
    {
        PlacementSystem.onOvergrownWarning -= OpenPopUp;
    }

    void Start()
    {
        ClosePopUp();
        popUpText = GetComponent<TextMeshProUGUI>();

        switch (LanguageManager.Instance.currentLanguage)
        {
            case Language.FI:
                popUpText.text = "Alue on umpeenkasvanut!";
                break;
            case Language.SW:
                popUpText.text = "Den är igenvuxen!";
                break;
            case Language.EN:
                popUpText.text = "It is too overgrown!";
                break;
        }
    }

    public void ClosePopUp()
    {
        this.gameObject.SetActive(false);
    }
    public void OpenPopUp()
    {
        this.gameObject.SetActive(true);
        Invoke("ClosePopUp", 1.5f);
    }
}
