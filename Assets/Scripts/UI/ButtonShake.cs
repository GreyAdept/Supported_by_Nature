using DG.Tweening;
using UnityEngine;

public class ButtonShake : MonoBehaviour
{
    public void ShakeButton(Transform button)
    {
        button.DOShakeScale(0.1f, 0.2f, 10, 90f, true, ShakeRandomnessMode.Full).SetUpdate(true);
    }
}

