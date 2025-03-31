using UnityEngine;
using UnityEngine.Events;

public class tileWeedsGrowth : MonoBehaviour
{
    public gameTile tile;

    public int growStage;
    private GameObject currentGrowth;
    public GameObject growStage1;
    public GameObject growStage2;
    public GameObject growStage3;

    private TurnManager tm;
    
    void Start()
    {
        tm = TurnManager.Instance;
        tm.onTurnChanged.AddListener(SpreadPlants);
        tile = transform.GetComponent<gameTile>();
        GrowWeeds(0);
    }
    
    private void GrowWeeds(int random)
    {
        float randomValue = Random.Range(0.0f, 1.0f);

        switch (tile.tileType) // <-- use different random values for water / wet areas
        {
            case tileManager.TileType.Water:

                if (randomValue > 0.85f)
                {
                    if (growStage < 3)
                    {
                        growStage++;
                        UpdateWeedObject();
                    }
                }
                break;


            case tileManager.TileType.Wetland:
                if (randomValue > 0.60f)
                {
                    if (growStage < 3)
                    {
                        growStage++;
                        UpdateWeedObject();
                    }
                }
                break;


            case tileManager.TileType.Forest:
                break;

        }

        
    }

    public void UpdateWeedObject()
    {
        switch (growStage)
        {
            case 0:
                Destroy(currentGrowth);
                break;
            case 1:
                Destroy(currentGrowth);
                currentGrowth = Instantiate(growStage1, tile.transform.position, Quaternion.identity);
                break;
            case 2:
                Destroy(currentGrowth);
                currentGrowth = Instantiate(growStage2, tile.transform.position, Quaternion.identity);
                break;
            case 3:
                Destroy(currentGrowth);
                currentGrowth = Instantiate(growStage3, tile.transform.position, Quaternion.identity);
                break;
        }
    }


    public void CutWeeds()
    {
        if (growStage > 0)
        {
            growStage = 1;
        }
        UpdateWeedObject();

    }

    public void SpreadPlants(int random)
    {   
        if (growStage > 0)
        {
            var adjacents = tile.adjacentTiles;
            int randomChoice = Random.Range(0, adjacents.Count - 1);
            var chosenTile = adjacents[randomChoice];
            chosenTile.GetComponent<tileWeedsGrowth>().GrowWeeds(0);
        }  
    }
        
}
