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

    private tileSelectedEffect effectHandler;
    
    // tile data
    public tileManager.TileType tileType;

   
    

    void Start()
    {
        tileManager = tileManager.Instance;
        rend = GetComponent<Renderer>();
        effectHandler = GetComponent<tileSelectedEffect>();

    }

    
    void Update()
    {   
        
        if (isHovered)
        {
            rend.material.color = Color.red;
            effectHandler.PlayParticle();
            StartCoroutine(clearHover());
        }
        else
        {   
            effectHandler.StopParticle();
            switch (tileType)
            {
                case tileManager.TileType.Forest:
                    rend.material.color = Color.green;
                    break;
                case tileManager.TileType.Water:
                    rend.material.color = Color.blue;
                    break;
                case tileManager.TileType.Wetland:
                    rend.material.color = Color.white;
                    break;
                default:
                    rend.material.color = Color.white;
                    break;
            }
        }
        
        
        
    }
    

    public string ReturnTileData()
    {
        return gridPosition.ToString();
    }

    IEnumerator clearHover()
    {
        isHovered = false;
        yield return new WaitForSeconds(0.1f);
    }
    
}
