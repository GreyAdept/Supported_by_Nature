using TMPro;
using UnityEngine;

public class ButtonTextLocalized : MonoBehaviour
{
    [SerializeField] private LocalizedText buttonText;
    private TMP_Text buttonTextObject;
    private void Start()
    {
        buttonTextObject = GetComponent<TMP_Text>();
        SetText();
    }
    private void SetText()
    {
        buttonTextObject.text = buttonText.GetText();
    }
}
