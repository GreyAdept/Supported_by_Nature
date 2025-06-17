using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class gameTile : MonoBehaviour
{
    //grid related
    public List<GameObject> adjacentTiles = new List<GameObject>();
    public Vector2Int gridPosition;

    //tile status
    public bool isSelected = false;
    public bool isHovered = false;

    //components
    private tileManager tileManager;
    private Renderer rend;
    
    //tile materials
    private tileSelectedEffect effectHandler;
    
    // tile data
    public tileManager.TileType tileType;
    public bool isNextToLand;
   
    //plant related
    public int overgrownState; // how much have the reeds grown
    public Plant grownPlant; //plant that the player can place
    public GameObject plantPrefab;
    [SerializeField] private int plantGrowStage;
    

    


    void Start()
    {   
        isNextToLand = false;
        grownPlant = null;
        tileManager = tileManager.Instance;
        
        
        TurnManager.Instance.onTurnChanged.AddListener(IncrementPlantGrowStage);
         

        foreach (GameObject t in adjacentTiles) //<-- check if tile is next to land
        {
            if (t.GetComponent<gameTile>().tileType == tileManager.TileType.Wetland)
            {
                isNextToLand = true;
            }
        }
    }


    
    void Update()
    {
        if (grownPlant)
        {
            plantGrowStage = grownPlant.plantGrowStage; //might not need to update this every frame
        }
        
    }
    
    public string ReturnTileData()
    {
        return gridPosition.ToString();
    }


    public void ClearHover()
    {
        //effectHandler.StopParticle();
        isHovered = false;
    }

    public void StartHover()
    {
        //effectHandler.PlayParticle(); it was replaced by tile selector sprite
    }

    public void IncrementPlantGrowStage(int random)
    {
        if (grownPlant && grownPlant.plantGrowStage < 3 && overgrownState < 3)
        {
            grownPlant.plantGrowStage++;
        }

        UpdatePlant();
    }

    public void UpdatePlant() //Update the plant object accordingly with a different model as it grows bigger
    {   
        if (grownPlant)
        {
            switch (grownPlant.plantGrowStage)
            {
                case 0:
                    plantPrefab = Instantiate(grownPlant.organismPrefab, this.transform.position, plantPrefab.transform.rotation);
                    break;
                case 1:
                    plantPrefab = Instantiate(grownPlant.organismPrefab, this.transform.position, plantPrefab.transform.rotation);
                    break;
                case 2:
                    Destroy(plantPrefab);
                    plantPrefab = Instantiate(grownPlant.plantGrowStagePrefab2, this.transform.position, plantPrefab.transform.rotation);
                    break;
                case 3:
                    Destroy(plantPrefab);
                    plantPrefab = Instantiate(grownPlant.plantGrowStagePrefab3, this.transform.position, plantPrefab.transform.rotation);
                    break;
               
            }
        }
    }


   
}
