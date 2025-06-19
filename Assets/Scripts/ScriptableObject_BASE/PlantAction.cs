using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantAction", menuName = "PlantAction")]
public class PlantAction : tileAction
{
    public Plant[] plants;
    private PlacementSystem placementSystem;

    //Moved this functionality to the "PlacementSystem" class.
    public override void affectTile(gameTile tile)
    {
        Debug.Log("temp");
    }

    
}