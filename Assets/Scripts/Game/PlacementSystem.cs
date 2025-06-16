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
        switch(inputManager.currentButton)
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
            //Debug.Log("effect trigger!");
        }
        else
        {
            Debug.Log("Not enough AP");
        }
    }
}
