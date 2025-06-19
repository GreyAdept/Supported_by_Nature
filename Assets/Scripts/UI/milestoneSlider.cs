using UnityEngine;

public class milestoneSlider : MonoBehaviour
{

    private UnityEngine.UI.Slider slider;

    void Awake()
    {
        MilestoneHandler.onBiodiversityChanged += (int context) => UpdateSlider(context);
    }

    private void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();    
    }

    private void UpdateSlider(int value)
    {
        slider.value = value;
    }



}
