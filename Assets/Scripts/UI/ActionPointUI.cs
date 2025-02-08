using TMPro;
using UnityEngine;

public class ActionPointUI : MonoBehaviour
{
    private TurnManager turnManager;
    [SerializeField] private TMP_Text apValueText;
    private void Start()
    {
        turnManager = TurnManager.Instance;
        turnManager.onActionPointsChanged.AddListener(UpdateActionPointsUI);
    }
    private void UpdateActionPointsUI(int actionPoints)
    {
        apValueText.text = actionPoints.ToString();
    }
}
