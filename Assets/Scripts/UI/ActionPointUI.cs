using TMPro;
using UnityEngine;
using DG.Tweening;

public class ActionPointUI : MonoBehaviour
{
    private TurnManager turnManager;
    [SerializeField] private TMP_Text apValueText;
    private int apGain;


    private void Awake()
    {
        MilestoneHandler.OnActionPointIncomeChanged += (int ctx) => apGain = ctx;
    }

    private void Start()
    {
        turnManager = TurnManager.Instance;
        turnManager.onActionPointsChanged.AddListener(UpdateActionPointsUI);
    }
    private void UpdateActionPointsUI(int actionPoints)
    {   
        apValueText.text = $"{actionPoints.ToString()} (+{apGain.ToString()})";
        PlayGainAPAnimation();
    }

    private void PlayGainAPAnimation()
    {
        apValueText.transform.DOShakeScale(0.5f, 0.2f);
    }
}
