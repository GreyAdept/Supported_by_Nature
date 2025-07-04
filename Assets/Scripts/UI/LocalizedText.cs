using UnityEngine;

[System.Serializable]
public class LocalizedText
{
    [TextArea(1, 10)] public string fi;
    [TextArea(1, 10)] public string sw;
    [TextArea(1, 10)] public string en;
    public string GetText()
    {
        if (LanguageManager.Instance == null) return fi;
        return GetText(LanguageManager.Instance.currentLanguage);
    }
    private string GetText(Language language)
    {
        switch(language)
        {
            case Language.FI:
                return fi;
            case Language.SW:
                return sw;
            case Language.EN:
                return en;
            default:
                return fi;
        }
    }
}
