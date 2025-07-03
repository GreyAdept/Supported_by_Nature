using TMPro;
using UnityEngine;

public class ButtonTextLocalized : MonoBehaviour
{
    [SerializeField] private LocalizedText buttonText;
    private TMP_Text buttonTextObject;
    private void Start()
    {
        buttonTextObject = GetComponent<TMP_Text>();
        //LanguageManager.Instance.onLanguageChanged.AddListener(SetText);
        LanguageManager.onLanguageChanged += SetText;
        SetText();
    }
    private void SetText()
    {
        buttonTextObject.text = buttonText.GetText();
    }

    private void OnDisable()
    {
        //LanguageManager.Instance.onLanguageChanged.RemoveListener(SetText);
        LanguageManager.onLanguageChanged -= SetText;
    }
}
