using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

public class ProgressBarController : MonoBehaviour
{
    public UnityEngine.UI.Slider progressSlider;
    private UnityEngine.UI.Image fillImage;

    public Color increaseColor = Color.green;
    public Color decreaseColor = Color.red;
    public float fadeDuration = 0.5f;

    public Color defaultBaseColor = new Color(0.3f, 0.3f, 0.3f); // dark when empty
    public Color defaultBrightColor = Color.white;               // bright when full

    private float previousValue;
    private Coroutine colorCoroutine;


    void Awake()
    {
        MilestoneHandler.onBiodiversityChanged += SetProgress;
    }

    private void OnDisable()
    {
        MilestoneHandler.onBiodiversityChanged -= SetProgress;
    }

    void Start()
    {
        if (progressSlider == null)
            progressSlider = GetComponent<UnityEngine.UI.Slider>();

        if (fillImage == null)
            fillImage = progressSlider.fillRect.GetComponent<UnityEngine.UI.Image>();

        previousValue = progressSlider.value;

        // Apply brightness-based color at startup
        Color currentBaseColor = Color.Lerp(defaultBaseColor, defaultBrightColor, previousValue);
        fillImage.color = currentBaseColor;
    }

    public void SetProgress(int newValue)
    {
        //newValue = Mathf.Clamp01(newValue);

        Color currentBaseColor = Color.Lerp(defaultBaseColor, defaultBrightColor, newValue);

        if (newValue != previousValue)
        {
            Color flashColor = newValue > previousValue ? increaseColor : decreaseColor;

            if (colorCoroutine != null)
                StopCoroutine(colorCoroutine);

            colorCoroutine = StartCoroutine(FlashAndFadeColor(flashColor, currentBaseColor));

            previousValue = newValue;
        }
        else
        {
            fillImage.color = currentBaseColor;
        }

        progressSlider.value = newValue;
    }

    IEnumerator FlashAndFadeColor(Color flashColor, Color targetColor)
    {
        fillImage.color = flashColor;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fillImage.color = Color.Lerp(flashColor, targetColor, timer / fadeDuration);
            yield return null;
        }

        fillImage.color = targetColor;
    }
}
