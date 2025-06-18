using System.Runtime.CompilerServices;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        //TurnManager.Instance.onActionPointsChanged.AddListener(PrintInfo);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            //TurnManager.Instance.gameState.currentActionPoints--;
            //TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints);
        }
    }
    private void PrintInfo(int currentAP)
    {
        //Debug.Log($"func triggered with ap {currentAP}, current ap {TurnManager.Instance.gameState.currentActionPoints}");
    }
}
