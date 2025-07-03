using TMPro;
using UnityEngine;

public class ScorePercentageText : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = (slider.normalizedValue * 100).ToString() + "%";
    }
}
