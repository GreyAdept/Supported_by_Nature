using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public int maxBiodiversity;

    [SerializeField] private int tileCount;
    [SerializeField] private Sprite milestoneLockedSprite;
    [SerializeField] private Sprite milestoneOneAvailable;
    [SerializeField] private Sprite milestoneTwoAvailable;
    [SerializeField] private Sprite milestoneThreeAvailable;

    private bool milestone1reward = false;
    [SerializeField] private GameObject cowCollection;
    [SerializeField] private GameObject fadeObject;
    [SerializeField] private GameObject endScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBiodiversity = 0;
        totalMilestoneProgress = 0;
        TurnManager.Instance.onTurnChanged.AddListener(ResetBiodiversity);
        TurnManager.Instance.onTurnChanged.AddListener(SpawnMilestoneReward);
        milestone1Button.image.sprite = milestoneLockedSprite;
        milestone2Button.image.sprite = milestoneLockedSprite;
        milestone3Button.image.sprite = milestoneLockedSprite;
        milestone1Button.interactable = false;
        milestone2Button.interactable = false;
        milestone3Button.interactable = false;

        tileCount = GameObject.FindGameObjectsWithTag("Tile").Length - 1;

        maxBiodiversity = tileCount * 4;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnMilestoneReward(int turnNum)
    {
        if(milestone1reward)
        {
            milestone1reward = false;
            cowCollection.SetActive(true);
        }
    }
    private void UpdateSlider()
    {
        milestoneSlider.value = currentBiodiversity;

        if (currentBiodiversity > maxBiodiversity * 0.25)
        {
            milestone1Button.interactable = true;
            milestone1Button.image.sprite = milestoneOneAvailable;
        }

        if (currentBiodiversity > maxBiodiversity * 0.50)
        {
            milestone2Button.interactable = true;
            milestone2Button.image.sprite = milestoneTwoAvailable;
        }

        if (currentBiodiversity > maxBiodiversity * 0.80)
        {
            milestone3Button.interactable = true;
            milestone3Button.image.sprite = milestoneThreeAvailable;
        }
    }

    public void ProgressMilestone(int milestone)
    {   
        
            switch (milestone)
            {
                case 1:
                    if (milestone1Progress < 3 && milestone1.isOn == false)
                    {
                        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
                        {
                            TurnManager.Instance.gameState.currentActionPoints -= 1;
                            milestone1Progress++;
                            milestone1Button.GetComponentInChildren<TextMeshProUGUI>().text = milestone1Progress + "/3 (AP)";
                            if (milestone1Progress >= 3)
                            {
                                milestone1.isOn = true;
                                milestone1Button.interactable = false;
                                //milestone1Button.gameObject.SetActive(false);
                                RandomEventSystem.instance.ForceNextEvent("kosteikolle_saapuu");
                                milestone1reward = true;
                                
                            }
                        }
                    }
                    
                    break;
                case 2:
                    if (milestone2Progress < 3 && milestone2.isOn == false)
                    {
                        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
                        {
                            TurnManager.Instance.gameState.currentActionPoints -= 1;
                            milestone2Progress++;
                            milestone2Button.GetComponentInChildren<TextMeshProUGUI>().text = milestone2Progress + "/3 (AP)";
                            if (milestone2Progress >= 3)
                            {
                                milestone2.isOn = true;
                                milestone2Button.interactable = false;
                                //milestone2Button.gameObject.SetActive(false);
                                RandomEventSystem.instance.ForceNextEvent("vesilinnut_saapuvat");
                                
                            }
                        }

                        
                    }
                    break;
                case 3:
                    if (milestone3Progress < 3 && milestone3.isOn == false)
                    {
                        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
                        {
                            TurnManager.Instance.gameState.currentActionPoints -= 1;
                            milestone3Progress++;
                            milestone3Button.GetComponentInChildren<TextMeshProUGUI>().text = milestone3Progress + "/3 (AP)";
                            if (milestone3Progress >= 3)
                            {
                                milestone3.isOn = true;
                                milestone3Button.interactable = false;
                                //milestone3Button.gameObject.SetActive(false);
                                StartEndSequence();
                                Debug.Log("you're winner");
                            }
                        }

                        
                    }
                    break;
            }
            TurnManager.Instance.onActionPointsChanged.Invoke(TurnManager.Instance.gameState.currentActionPoints);
            //totalMilestoneProgress += 1;
            UpdateSlider();
            
        
        
    }


    private void ResetBiodiversity(int random)
    {
        currentBiodiversity = 0;
        Invoke("UpdateSlider", 0.6f);
    }

    //very shitty made at 3am
    private void StartEndSequence()
    {
        fadeObject.GetComponent<Fader>().FadeScreen();
    }
    public void EnableEndScreen()
    {
        endScreen.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    //

    public void IncrementBiodiversity(int amount)
    {
        currentBiodiversity += amount;
    }

 
}
