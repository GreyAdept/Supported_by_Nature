using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTile : MonoBehaviour
{
   
    public List<GameObject> adjacentTiles = new List<GameObject>();
    public Vector2Int gridPosition;
    public bool isSelected = false;
    public bool isHovered = false;

    private tileManager tileManager;
    private Renderer rend;
    
    //tile materials
    private tileSelectedEffect effectHandler;
    
    // tile data
    public tileManager.TileType tileType;
    public bool isNextToLand;
   
    //plant related data
    public List<GameObject> plants = new List<GameObject>();

    


    void Start()
    {   
        isNextToLand = false;
        tileManager = tileManager.Instance;
        rend = GetComponent<Renderer>();
        effectHandler = GetComponent<tileSelectedEffect>();
         

        foreach (GameObject t in adjacentTiles) //<-- check if tile is next to land
        {
            if (t.GetComponent<gameTile>().tileType == tileManager.TileType.Wetland)
            {
                isNextToLand = true;
            }
        }


        switch (tileType) //<-- assign materials based on tiletype and move slightly for terrain height
        {
            case tileManager.TileType.Forest:
                rend.material = tileManager.materialForest;
                transform.Translate(Vector3.up*0.1f, Space.Self);
                break;
            case tileManager.TileType.Water:
                rend.material = tileManager.materialWater;
                transform.Translate(Vector3.down*0.1f, Space.Self);
                break;
            case tileManager.TileType.Wetland:
                rend.material = tileManager.materialWetland;
                break;
            default:
                rend.material.color = Color.white;
                break;
        }
        
    }


    
    void Update()
    {

    }
    
    public string ReturnTileData()
    {
        return gridPosition.ToString();
    }

    public void ClearHoverHelper()
    {   
        Invoke("ClearHover", 0.5f);
    }

    public void ClearHover()
    {
        effectHandler.StopParticle();
        isHovered = false;
    }

    public void StartHover()
    {
        effectHandler.PlayParticle();
    }



   
}
