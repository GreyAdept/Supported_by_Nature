using UnityEngine;

public class LanguageButton : MonoBehaviour
{

    public static event System.Action<Language> OnLanguageButtonHit;
    public Language language;

    public void ChangeLanguage()
    {
        OnLanguageButtonHit?.Invoke(language);
    }
}
