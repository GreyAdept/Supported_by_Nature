using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public Language currentLanguage = Language.FI;
    public UnityEvent onLanguageChanged;
    [SerializeField] private DialogueDatabase[] dialogueDatabases;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("duplicate languagemanager");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void SwitchLanguage(string language)
    {

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
        onLanguageChanged?.Invoke();
    }
}
public enum Language
{
    FI,
    SW,
    EN,
}
