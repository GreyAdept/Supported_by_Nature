using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCG;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Button[] menuButtons;
    [SerializeField] private TMP_Text startGame;
    [SerializeField] private TMP_Text quitGame;
    [SerializeField] private TMP_Text credits;
    [SerializeField] private LocalizedText startGameText;
    [SerializeField] private LocalizedText quitGameText;
    [SerializeField] private LocalizedText creditsText;
    private void Start()
    {
        if (fadeCG != null)
        {
            fadeCG.blocksRaycasts = false;
            fadeCG.alpha = 0;
        }
        ToggleButtons(true);
        UpdateText();
        LanguageManager.Instance.onLanguageChanged.AddListener(UpdateText);
    }
    private void UpdateText()
    {
        startGame.text = startGameText.GetText();
        quitGame.text = quitGameText.GetText();
        credits.text = creditsText.GetText();
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
