using UnityEngine;

[CreateAssetMenu(fileName = "RemovePlantAction", menuName = "RemovePlantAction")]
public class RemovePlantAction : tileAction
{
    public override void affectTile(gameTile tile)
    {
        /*
        foreach (GameObject plant in tile.plants)
        {   
            Destroy(plant);
        }
        tile.plants.Clear();
        */

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

        


        //TurnManager.Instance.onActionPointsChanged.Invoke(2);
    }
}
