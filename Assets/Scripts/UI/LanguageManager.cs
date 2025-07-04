using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public Language currentLanguage = Language.FI;
    public static event System.Action onLanguageChanged;
    [SerializeField] private DialogueDatabase[] dialogueDatabases;

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LanguageButton.OnLanguageButtonHit += SwitchLanguage;

    }



    private void OnDisable()
    {
        LanguageButton.OnLanguageButtonHit -= SwitchLanguage;
    }

    public void SwitchLanguage(Language language)
    {

        currentLanguage = language;
        /*
        switch (language)
        {
            case "fi":
                currentLanguage = Language.FI;
                break;
            case "sw":
                currentLanguage = Language.SW;
                break;
            case "en":
                currentLanguage = Language.EN;
                break;
        }
        */
        onLanguageChanged?.Invoke();
    }
}
public enum Language
{
    FI,
    SW,
    EN,
}
