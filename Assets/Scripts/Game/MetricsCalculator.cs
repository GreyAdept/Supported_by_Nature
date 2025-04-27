using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsCalculator : MonoBehaviour
{   
    public gameTile tile;
    public tileWeedsGrowth tileWeeds;

    public int tileBiodiversity;
    void Start()
    {   
        TurnManager.Instance.onTurnChanged.AddListener(DelayedCalculateBiodiversity);
        tile = GetComponent<gameTile>();
        tileWeeds = GetComponent<tileWeedsGrowth>();
    }

    public void DelayedCalculateBiodiversity(int random)
    {
        Invoke("CalculateBiodiversity", 0.5f);
    }
    
    public void CalculateBiodiversity()
    {
        int diversity = 1;

        if (tile.grownPlant != null)
        {
            switch (tile.grownPlant.plantGrowStage)
            {
                case 0:
                    diversity++;
                    break;
                case 1:
                    diversity += 2;
                    break;
                case 2:
                    diversity += 3;
                    break;
                case 3:
                    diversity += 3;
                    break;
                default:
                    diversity++;
                    break;
            }
            
        }
        switch (tileWeeds.growStage)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                diversity -= 1;
                break;
            default:
                break;
        }

        tileBiodiversity = diversity;
        TurnManager.Instance.milestoneHandler.IncrementBiodiversity(diversity);


    }
}

  
