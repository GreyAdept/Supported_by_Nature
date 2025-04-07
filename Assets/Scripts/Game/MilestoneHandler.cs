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
    
    public int totalMilestoneProgress;
    public int highestMilestoneReached;

    public int currentBiodiversity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBiodiversity = 0;
        totalMilestoneProgress = 0;
        TurnManager.Instance.onTurnChanged.AddListener(ResetBiodiversity);
        //TurnManager.Instance.onTurnChanged.AddListener(UpdateSlider);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSlider()
    {
        milestoneSlider.value = currentBiodiversity;

        if (currentBiodiversity > 100f)
        {
            milestone1.isOn = true;
        }

        if (currentBiodiversity > 200f)
        {
            milestone2.isOn = true;
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


    private void ResetBiodiversity(int random)
    {
        currentBiodiversity = 0;
        Invoke("UpdateSlider", 0.6f);
    }

    private void DelayedReset()
    {
        
    }
    
    
    public void IncrementBiodiversity(int amount)
    {
        currentBiodiversity += amount;
    }
}
