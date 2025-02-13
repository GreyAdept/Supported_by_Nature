using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTile : MonoBehaviour
{
   
    public List<GameObject> adjacentTiles = new List<GameObject>();
    public Vector2Int gridPosition;
    public bool isSelected = false;
    public bool isHovered = false;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (isHovered)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(clearHover());
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
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
