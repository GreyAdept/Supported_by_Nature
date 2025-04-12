using UnityEngine;
using UnityEngine.UI;

public class MilestoneHandler : MonoBehaviour
{
    public Button milestoneButton;
    public Slider milestoneSlider;

    public Toggle milestone1;
    public Toggle milestone2;
    public Toggle milestone3;
    
    public Button milestone1Button;
    public Button milestone2Button;
    public Button milestone3Button;
    
    public int milestone1Progress;
    public int milestone2Progress;
    public int milestone3Progress;
    
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
        milestone1Button.interactable = false;
        milestone2Button.interactable = false;
        milestone3Button.interactable = false;
        
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
            milestone1Button.interactable = true;
        }

        if (currentBiodiversity > 200f)
        {
            milestone2.isOn = true;
            milestone2Button.interactable = true;
        }

        if (currentBiodiversity > 300f)
        {
            milestone3.isOn = true;
            milestone3Button.interactable = true;
        }
    }

    public void ProgressMilestone(int milestone)
    {   
        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
        {   
            TurnManager.Instance.gameState.currentActionPoints -= 1;
            switch (milestone)
            {
                case 1:
                    if (milestone1Progress < 3)
                    {
                        milestone1Progress++;
                    }
                    
                    break;
                case 2:
                    if (milestone2Progress < 3)
                    {
                        milestone2Progress++;
                    }
                    break;
                case 3:
                    if (milestone3Progress < 3)
                    {
                        milestone3Progress++;
                    }
                    break;
            }
            TurnManager.Instance.onActionPointsChanged.Invoke(TurnManager.Instance.gameState.currentActionPoints);
            //totalMilestoneProgress += 1;
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
