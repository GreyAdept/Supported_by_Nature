using TMPro;
using UnityEngine;

public class PopUp_CutNothing : MonoBehaviour
{

    private TextMeshProUGUI popUpText;

    void Awake()
    {
        PlacementSystem.onCutNothing += OpenPopUp;
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        PlacementSystem.onCutNothing -= OpenPopUp;
    }

    void Start()
    {
        ClosePopUp();
        popUpText = GetComponent<TextMeshProUGUI>();

        switch (LanguageManager.Instance.currentLanguage)
        {
            case Language.FI:
                popUpText.text = "Ei tarvitse niittoa.";
                break;
            case Language.SW:
                popUpText.text = "Detta kräver inte gräsklippning";
                break;
            case Language.EN:
                popUpText.text = "This doesn't require cutting.";
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
