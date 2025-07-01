using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    private tileManager tm;
    public Plant[] plants;
    private InputManager inputManager;

    public static event System.Action onAPWarning;
    public static event System.Action onExistingPlantWarning;
    public static event System.Action onOvergrownWarning;
    public static event System.Action onCutNothing;

    public static event System.Action onActionDone;
    public static event System.Action onBigWeedCut;
    public static event System.Action onPlantPlaced;


    void Start()
    {
        tm = tileManager.Instance;
        inputManager = InputManager.Instance;
        InputManager.OnPointerReleased += ExecuteAction;
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
            onAPWarning?.Invoke();
            return;
        }
       
        if (tile.grownPlant != null)
        {
            onExistingPlantWarning?.Invoke();
            return;
        }
        
        if (tile.overgrownState >= 3)
        {
            onOvergrownWarning?.Invoke();
            return;
        }
                            
        TurnManager.Instance.gameState.currentActionPoints -= 1; 
        TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints); //fire event when action points change
        int randomIndex = Random.Range(0, 2);
        //Debug.Log(randomIndex, this);
        tile.grownPlant = plants[randomIndex]; 
        tile.grownPlant.plantGrowStage = 0;
        //tile.plantPrefab = plants[randomIndex].organismPrefab;
        onPlantPlaced?.Invoke();
        tile.UpdatePlant(); 
                            
    }

    private void CutPlant()
    {   

        gameTile tile = tm.selectedTile;
        var weedScript = tile.GetComponent<tileWeedsGrowth>();

        if (weedScript.growStage <= 1)
        {
            onCutNothing?.Invoke();
            return;
        }

        if (TurnManager.Instance.gameState.currentActionPoints < 1)
        {
            onAPWarning?.Invoke();
            return;
        }

        TurnManager.Instance.gameState.currentActionPoints -= 1;
        TurnManager.Instance.onActionPointsChanged?.Invoke(TurnManager.Instance.gameState.currentActionPoints);
        if (weedScript.growStage == 3)
        {
            onBigWeedCut?.Invoke();
        }
        weedScript.growStage = 1;
        onActionDone?.Invoke();
        weedScript.UpdateWeedObject();
    }
}
