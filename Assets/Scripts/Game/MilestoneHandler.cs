using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneHandler : MonoBehaviour
{
    public Button milestoneButton;
    public Slider milestoneSlider;

    public Toggle milestone1;
    public Toggle milestone2;
    public Toggle milestone3;
    public Toggle milestone4;
    public Toggle milestone5;

    public int totalMilestoneProgress;
    public int highestMilestoneReached;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalMilestoneProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSlider()
    {
        milestoneSlider.value = totalMilestoneProgress;
        if (totalMilestoneProgress == 2)
        {
            milestone1.isOn = true;
        }
        else if (totalMilestoneProgress == 4)
        {
            milestone2.isOn = true;
        }
        else if (totalMilestoneProgress == 6)
        {
            milestone3.isOn = true;
        }
        else if (totalMilestoneProgress == 8)
        {
            milestone4.isOn = true;
        }
        else if (totalMilestoneProgress == 10)
        {
            milestone5.isOn = true;
        }
    }

    public void ProgressMilestone()
    {
        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
        {
            TurnManager.Instance.gameState.currentActionPoints -= 1;
            TurnManager.Instance.onActionPointsChanged.Invoke(TurnManager.Instance.gameState.currentActionPoints);
            totalMilestoneProgress += 1;
            UpdateSlider();
            
        }
        
    }
}
