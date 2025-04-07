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
                if (tile.overgrownState < 2)
                {
                    TurnManager.Instance.gameState.currentActionPoints -= 1;
                    TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints);
                    int randomIndex = Random.Range(0, plants.Length - 1);
                    GameObject plantPrefab = plants[randomIndex].organismPrefab;
                    GameObject newPlant = Instantiate(plantPrefab, tile.gameObject.transform.position, Quaternion.identity);
                    tile.grownPlant = plants[randomIndex];
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