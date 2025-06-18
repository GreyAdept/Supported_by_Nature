using TMPro;
using UnityEngine;

public class TurnChangeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text turnCounterText;
    private void Awake()
    {   
        TurnManager.OnTurnChanged += () => UpdateTurnCounterUI();
    }

    private void Start()
    {
        turnCounterText.text = GameMaster.Instance.gameState.currentTurn.ToString();
    }
    private void UpdateTurnCounterUI()
    {
        Debug.Log("end turn", this);
        turnCounterText.text = GameMaster.Instance.gameState.currentTurn.ToString();
    }
}
