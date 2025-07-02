using UnityEngine;
using DG.Tweening;

public class clockArrowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var tween = this.transform.DOLocalRotate(new Vector3(0, 0, -360f), 20f, RotateMode.FastBeyond360);
        tween.SetEase(Ease.Linear);
        tween.onComplete += () => { Rotate(); };
        tween.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Rotate()
    {

        
        
    }
}
