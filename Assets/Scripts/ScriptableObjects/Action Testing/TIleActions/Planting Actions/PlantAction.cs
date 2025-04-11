using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantAction", menuName = "PlantAction")]
public class PlantAction : tileAction
{
    public Plant[] plants;

    public override void affectTile(gameTile tile)
    {   
        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
        {
            if (tile.grownPlant == null)
            {
                if (tile.overgrownState < 3)
                {
                    TurnManager.Instance.gameState.currentActionPoints -= 1;
                    TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints);
                    int randomIndex = Random.Range(0, plants.Length - 1);
                    tile.grownPlant = plants[randomIndex];
                    tile.grownPlant.plantGrowStage = 0;
                    tile.plantPrefab = plants[randomIndex].organismPrefab;
                    tile.UpdatePlant();
                    
                }
                else
                {
                    TurnManager.Instance.warningMessages.ShowWarningOvergrown();
                    
                }
                
            }
            else
            {
                TurnManager.Instance.warningMessages.ShowWarningExistingPlant();
            }
            
        }
        else
        {
            TurnManager.Instance.warningMessages.ShowWarningAP();
        }
    }
}