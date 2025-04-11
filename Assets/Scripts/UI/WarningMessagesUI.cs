using UnityEngine;

public class WarningMessagesUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject WarningAP;
    public GameObject WarningOvergrown;
    public GameObject WarningExistingPlant;

    void Start()
    {
        HideWarnings();
    }

    public void ShowWarningAP()
    {
        WarningAP.SetActive(true);
        Invoke("HideWarnings", 1.2f);
    }

    public void ShowWarningOvergrown()
    {
        WarningOvergrown.SetActive(true);
        Invoke("HideWarnings", 1.2f);
    }

    public void ShowWarningExistingPlant()
    {
        WarningExistingPlant.SetActive(true);
        Invoke("HideWarnings", 1.2f);
    }


    private void HideWarnings()
    {
        WarningAP.SetActive(false);
        WarningOvergrown.SetActive(false);
        WarningExistingPlant.SetActive(false);
    }
}  
