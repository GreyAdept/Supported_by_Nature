using UnityEngine;

[CreateAssetMenu(fileName = "RemovePlantAction", menuName = "RemovePlantAction")]
public class RemovePlantAction : tileAction
{   

    //Moved this functionality to the "PlacementSystem" class 
    public override void affectTile(gameTile tile)
    {
        /*
        foreach (GameObject plant in tile.plants)
        {   
            Destroy(plant);
        }
        tile.plants.Clear();
        */

        

        


        //TurnManager.Instance.onActionPointsChanged.Invoke(2);
    }
}
