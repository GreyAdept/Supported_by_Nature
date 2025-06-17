using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    private tileManager tm;
    public Plant[] plants;
    private InputManager inputManager;
    
    


    void Start()
    {
        tm = tileManager.Instance;
        inputManager = InputManager.Instance;
        inputManager.onPointerReleased.AddListener(ExecuteAction);
    }


    void Update()
    {
        
    }

    public void ExecuteAction()
    {
        switch(inputManager.currentButton) //fire the correct action based on currently selected action button
        {
            case ButtonType.plant:
                PlacePlant();
                break;
            case ButtonType.cut:
                CutPlant();
                break;
        }
    }

    private void PlacePlant()
    {

        gameTile tile = tm.selectedTile;

        if (TurnManager.Instance.gameState.currentActionPoints < 1)
        {
            TurnManager.Instance.warningMessages.ShowWarningAP();
            return;
        }
       
        if (tile.grownPlant != null)
        {
            TurnManager.Instance.warningMessages.ShowWarningExistingPlant();
            return;
        }
        
        if (tile.overgrownState >= 3)
        {
            TurnManager.Instance.warningMessages.ShowWarningOvergrown();
            return;
        }
                            
        TurnManager.Instance.gameState.currentActionPoints -= 1; 
        TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints); //fire event when action points change
        int randomIndex = Random.Range(0, plants.Length - 1); 
        tile.grownPlant = plants[randomIndex]; 
        tile.grownPlant.plantGrowStage = 0; 
        tile.plantPrefab = plants[randomIndex].organismPrefab;
        tile.UpdatePlant(); 
                            
    }

    private void CutPlant()
    {
        gameTile tile = tm.selectedTile;

        if (TurnManager.Instance.gameState.currentActionPoints >= 1)
        {
            TurnManager.Instance.gameState.currentActionPoints -= 1;
            TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints);

            var weedScript = tile.GetComponent<tileWeedsGrowth>();
            weedScript.growStage = 1;
            weedScript.UpdateWeedObject();
            
        }
        else
        {
            Debug.Log("Not enough AP");
        }
    }
}
