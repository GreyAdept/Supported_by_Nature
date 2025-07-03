using UnityEngine;
using DG.Tweening;

public class clockArrowScript : MonoBehaviour
{

    private DG.Tweening.Tween rotationTween;


    private void Awake()
    {
        rotationTween = this.transform.DOLocalRotate(new Vector3(0, 0, -360f), 20f, RotateMode.FastBeyond360);
        rotationTween.SetEase(Ease.Linear);

        rotationTween.onComplete += Restart;
        GameMaster.OnPaused += OnPause;
    }


    private void Start()
    {
        rotationTween.Pause();
    }

    private void Restart()
    {
        rotationTween?.Restart();
    }

    private void OnPause(bool ctx)
    {
        if (ctx)
        {
            rotationTween.Pause();
        }
        else
        {
            rotationTween.Play();
        }
    }

    private void OnDisable()
    {
        rotationTween.onComplete -= Restart;
        GameMaster.OnPaused -= OnPause;
    }
}
