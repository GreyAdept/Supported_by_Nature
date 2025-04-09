using DG.Tweening;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    public void ShakeButton(Transform button)
    {
        button.DOShakeScale(0.1f, 0.2f, 10, 90f, true, ShakeRandomnessMode.Full).SetUpdate(true);
    }
    public void PlaySound(string soundID)
    {
        SoundManager.Instance.PlayUISound(soundID);
    }
}

