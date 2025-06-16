using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantAction", menuName = "PlantAction")]
public class PlantAction : tileAction
{
    public Plant[] plants;
    private PlacementSystem placementSystem;

    public override void affectTile(gameTile tile)
    {
        Debug.Log("temp");
    }

    
}