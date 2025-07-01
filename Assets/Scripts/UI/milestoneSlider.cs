using UnityEngine;
using DG.Tweening;

public class milestoneSlider : MonoBehaviour
{

    private UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image fillImage;

    void Awake()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();

        MilestoneHandler.onBiodiversityChanged += (int context) =>
        {
            if (context > slider.value)
            {
                FlashGreen();
            }
            else if (context < slider.value)
            {
                FlashRed();
            }
            UpdateSlider(context);
        };
    }

    private void Start()
    {
        
    }

    private void UpdateSlider(int value)
    {
        slider.value = value;
    }

    private void FlashGreen()
    {
        fillImage.DOColor(Color.green, 1f).onComplete += () => { ReturnColor(); };
    }

    private void FlashRed()
    {
        fillImage.DOColor(Color.red, 1f).onComplete += () => { ReturnColor(); };
    }

    private void ReturnColor()
    {
        fillImage.DOColor(Color.white, 1f);
    }

}
