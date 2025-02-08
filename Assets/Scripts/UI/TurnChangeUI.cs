using TMPro;
using UnityEngine;

public class TurnChangeUI : MonoBehaviour
{
    private TurnManager turnManager;
    [SerializeField] private TMP_Text turnCounterText;
    private void Start()
    {
        turnManager = TurnManager.Instance;
        turnManager.onTurnChanged.AddListener(UpdateTurnCounterUI);
    }
    private void UpdateTurnCounterUI(int currentTurn)
    {
        turnCounterText.text = currentTurn.ToString();
    }
}
