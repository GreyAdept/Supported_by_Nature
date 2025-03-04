using UnityEngine;

[CreateAssetMenu(fileName = "PlantAction", menuName = "PlantAction")]
public class PlantAction : tileAction
{
    public Plant plant;

    public override void affectTile(gameTile tile)
    {
        GameObject plantPrefab = plant.organismPrefab;
        GameObject newPlant = Instantiate(plantPrefab,tile.gameObject.transform.position, Quaternion.identity);
        tile.plants.Add(newPlant);
    }
}