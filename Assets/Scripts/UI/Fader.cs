using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCG;
    [SerializeField] private float fadeDuration = 0.5f;
    public static event System.Action onFaded;
    private void Start()
    {
        if (fadeCG != null)
        {
            fadeCG.blocksRaycasts = false;
            fadeCG.alpha = 0;
        }
    }
    public void FadeScreen()
    {
        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {
        fadeCG.blocksRaycasts = true; //doesnt seem to work
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCG.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCG.alpha = 1f;
        onFaded?.Invoke();
    }
}
