using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MilestoneHandler : MonoBehaviour
{
    #region UI references -> todo: remove hardcoded references

    public Slider milestoneSlider;

    public Toggle milestone1;
    public Toggle milestone2;
    public Toggle milestone3;

    public Button milestone1Button;
    public Button milestone2Button;
    public Button milestone3Button;
    #endregion


    public int milestone1Progress;
    public int milestone2Progress;
    public int milestone3Progress;

    public int totalMilestoneProgress;
    public int highestMilestoneReached;

    public int currentBiodiversity;
    public int maxBiodiversity;

    [SerializeField] private int tileCount;
    public Sprite milestoneLockedSprite;
    public Sprite milestoneOneAvailable;
    public Sprite milestoneTwoAvailable;
    public Sprite milestoneThreeAvailable;

    private bool milestone1reward = false;
    [SerializeField] private GameObject cowCollection;
    public GameObject fadeObject;
    [SerializeField] private GameObject endScreen;

    private List<MetricsCalculator> gameTileMetrics = new List<MetricsCalculator>();
    private List<GameObject> gameTiles = new List<GameObject>();

    public static event System.Action<int> onBiodiversityChanged;
    public static event System.Action onFirstMilestoneTriggered;
    public static event System.Action onTutorialDone;
    private bool tutorialTrigger = false;

    public static event System.Action<int> OnActionPointIncomeChanged;

    private int baseApGain = 1;
    [SerializeField] private int _apGain;
    private int tickTimer;
    public int ApGain
    {
        get { return _apGain; }
        set
        {
            OnActionPointIncomeChanged?.Invoke(value);
            _apGain = value;
        }
    }

   
    private void Awake()
    {
        Fader.onFaded += () => EnableEndScreen();
        //TurnManager.OnTurnChanged += () => UpdateBiodiversityFromTiles();
        PlacementSystem.onActionDone += () =>
        {
            UpdateUI();
            CalculateAPIncome();
        };
        

        PlacementSystem.onPlantPlaced += () =>
        {
            //currentBiodiversity += 5;
            ChangeScore(5);
            CalculateAPIncome();
            //onBiodiversityChanged?.Invoke(currentBiodiversity);
        };

        tileWeedsGrowth.OnGrowStage3Complete += () =>
        {
            //currentBiodiversity -= 10;
            ChangeScore(-10);
            CalculateAPIncome();
            //onBiodiversityChanged?.Invoke(currentBiodiversity);
        };

        PlacementSystem.onBigWeedCut += () =>
        {
            ChangeScore(10);
            CalculateAPIncome();
        };

        ClockScript.OnSecondsChanged += (int ctx) =>
        {
            tickTimer++;
            if (tickTimer > 10)
            {
                tickTimer = 0;
                AddAP();        
            }
            
        };

        GameState.OnEventChoiceMade += (AnswerCategory ctx) =>
        {
            switch (ctx)
            {
                case AnswerCategory.Good:
                    currentBiodiversity += 20;
                    break;
                case AnswerCategory.Bad:
                    currentBiodiversity -= 20;
                    break;
                case AnswerCategory.Neutral:
                    currentBiodiversity += 10;
                    break;
            }
            UpdateUI();
            ClampScore();
            CalculateAPIncome();
            onBiodiversityChanged.Invoke(currentBiodiversity);
        };

        
    }

    void Start()
    {
        currentBiodiversity = 0;
        totalMilestoneProgress = 0;

        //TurnManager.Instance.onTurnChanged.AddListener(ResetBiodiversity);
        TurnManager.Instance.onTurnChanged.AddListener(SpawnMilestoneReward);
        //TurnManager.Instance.onTurnChanged.AddListener(UpdateBiodiversityFromTiles);
        

        milestone1Button.image.sprite = milestoneLockedSprite;
        milestone2Button.image.sprite = milestoneLockedSprite;
        milestone3Button.image.sprite = milestoneLockedSprite;

        milestone1Button.interactable = false;
        milestone2Button.interactable = false;
        milestone3Button.interactable = false;

        tileCount = GameObject.FindGameObjectsWithTag("Tile").Length - 1;

        maxBiodiversity = tileCount * 4;

        foreach (var tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            gameTiles.Add(tile);
        }
    }
    
    private void ChangeScore(int score)
    {
        currentBiodiversity += score;
        ClampScore();
        onBiodiversityChanged?.Invoke(currentBiodiversity);
        UpdateUI();
    }


    private void ClampScore()
    {
        int lowerClamp = 0;
        
        if (milestone1.isOn)
        {
            lowerClamp = 97;
        }
        if (milestone2.isOn)
        {
            lowerClamp = 194;
        }
        if (milestone3.isOn)
        {
            lowerClamp = 250;
        }

        if (currentBiodiversity < lowerClamp)
        {
            currentBiodiversity = lowerClamp;
        }

    }

   
    private void SpawnMilestoneReward(int turnNum)
    {
        if(milestone1reward)
        {
            milestone1reward = false;
            cowCollection.SetActive(true);
        }
    }
    private void UpdateUI()
    {
        //milestoneSlider.value = currentBiodiversity;

        if (currentBiodiversity >= 97)
        {
            milestone1Button.interactable = true;
            milestone1Button.image.sprite = milestoneOneAvailable;
            if (!tutorialTrigger)
            {
                onFirstMilestoneTriggered?.Invoke();
            }
            tutorialTrigger = true;
            
        }

        if (currentBiodiversity >= 194)
        {
            milestone2Button.interactable = true;
            milestone2Button.image.sprite = milestoneTwoAvailable;
        }

        if (currentBiodiversity >= 250)
        {
            milestone3Button.interactable = true;
            milestone3Button.image.sprite = milestoneThreeAvailable;
        }
    }

    public void ProgressMilestone(int milestone) //This method is a 5-star spaghetti dinner, fix to be done
    {   
        
            switch (milestone)
            {
                case 1:
                    if (milestone1Progress < 3 && milestone1.isOn == false)
                    {
                        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
                        {
                            onTutorialDone?.Invoke();
                            TurnManager.Instance.gameState.currentActionPoints -= 1;
                            milestone1Progress++;
                            milestone1Button.GetComponentInChildren<TextMeshProUGUI>().text = milestone1Progress + "/3 (AP)";
                            if (milestone1Progress >= 3)
                            {
                                milestone1.isOn = true;
                                milestone1Button.interactable = false;
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
                                StartEndSequence();
                                Debug.Log("you're winner");
                            }
                        }

                        
                    }
                    break;
            }
            TurnManager.Instance.onActionPointsChanged.Invoke(TurnManager.Instance.gameState.currentActionPoints);
  
    }

    private void CalculateAPIncome()
    {
        int tempIncome = 0;

        foreach (var tile in gameTiles)
        {
            Debug.Log("tile found");

            var tileScript = tile.GetComponent<gameTile>();

            if (tileScript.grownPlant == null) continue;

            if (tileScript.plantGrowStage >= 3)
            {
                tempIncome++;
            }
        }

        ApGain = baseApGain + tempIncome;

    }

    private void AddAP()
    {
        TurnManager.Instance.gameState.currentActionPoints += ApGain;
        TurnManager.Instance.onActionPointsChanged.Invoke(TurnManager.Instance.gameState.currentActionPoints);

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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    //

    public void IncrementBiodiversity(int amount)
    {
        currentBiodiversity += amount;
    }

    private void UpdateBiodiversityFromTiles()
    {
        //FixedScoreCalculator();
        //onBiodiversityChanged.Invoke(currentBiodiversity);
        UpdateUI();

    }
    /*
    private void FixedScoreCalculator()
    {
        int currentScore = 0;
        foreach (var tile in gameTiles)
        {

            int tileScore = 0;

            var gameTile = tile.GetComponent<gameTile>();
            var tileWeed = tile.GetComponent<tileWeedsGrowth>();

            if (gameTile.grownPlant)
            {
                tileScore = (gameTile.grownPlant.plantGrowStage + 1);
            }
            else
            {
                tileScore = 0;
            }
            
            if (tileWeed.growStage >= 3)
            {
                tileScore -= 1;
            }
            //Debug.Log(tileScore);
            currentScore += tileScore;
        }

        currentBiodiversity = currentScore;
    }
    */

    [ContextMenu("Dev Win Game Cheat")] void DevWinGame()
    {
        milestone3Progress = 3;
        milestone3.isOn = true;
    }

   

 
}
