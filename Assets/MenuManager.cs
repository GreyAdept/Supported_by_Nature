using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCG;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Button[] menuButtons;
    private void Start()
    {
        if (fadeCG != null)
        {
            fadeCG.blocksRaycasts = false;
            fadeCG.alpha = 0;
        }
        ToggleButtons(true);
    }
    public void StartGame(int sceneNumber)
    {
        ToggleButtons(false);
        StartCoroutine(FadeToBlackAndLoad(sceneNumber));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator FadeToBlackAndLoad(int sceneNumber)
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
        SceneManager.LoadScene(sceneNumber);
    }
    private void ToggleButtons(bool state)
    {
        if (menuButtons != null)
        {
            foreach (Button button in menuButtons)
            {
                button.interactable = state;
            }
        }
    }
}
