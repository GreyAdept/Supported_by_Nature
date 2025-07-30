using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;




public class QualityManager : MonoBehaviour
{
    private int qualityLevel;
    private Toggle toggle;
    private static SettingObject currentSettings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        toggle = GameObject.Find("QualityToggleButton").GetComponentInChildren<Toggle>();
        if (currentSettings != null && currentSettings.qualityLevel == 2)
        {
            toggle.isOn = true;
        }
        toggle.onValueChanged.AddListener(ToggleLowGraphicsMode);
    }
  
    void Awake()
    {
        if (currentSettings == null)
        {
            Debug.Log("First time awaken!");
            qualityLevel = QualitySettings.GetQualityLevel();
            currentSettings = ScriptableObject.CreateInstance<SettingObject>();
            currentSettings.defaultQualityLevel = qualityLevel;
            currentSettings.qualityLevel = qualityLevel;
        }
       
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(ToggleLowGraphicsMode);
    }


    public void ToggleLowGraphicsMode(bool toggle)
    {
        Debug.Log("toggled!");

        if (toggle)
        {
            QualitySettings.SetQualityLevel(2); //enable low setting
            currentSettings.qualityLevel = 2;
        }
        else 
        {
            QualitySettings.SetQualityLevel(currentSettings.defaultQualityLevel); //back to default setting
            currentSettings.qualityLevel = currentSettings.defaultQualityLevel;
        }
    }
}

public class SettingObject : ScriptableObject //helper object for storing values between scenes
{
    public int defaultQualityLevel;
    public int qualityLevel;

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }
}

