using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopUp_AP : MonoBehaviour
{

    private TextMeshProUGUI popUpText;
    
    void Awake()
    {
        PlacementSystem.onAPWarning += OpenPopUp;
    }

    private void OnDisable()
    {
        PlacementSystem.onAPWarning -= OpenPopUp;
    }

    void Start()
    {   
        ClosePopUp();
        popUpText = GetComponent<TextMeshProUGUI>();

        switch(LanguageManager.Instance.currentLanguage)
        {
            case Language.FI:
                popUpText.text = "Ei tarpeeksi energiaa!";
                break;
            case Language.SW:
                popUpText.text = "Ingen mer energi!";
                break;
            case Language.EN:
                popUpText.text = "Not enough energy!";
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
