using System.Collections.Generic;
using UnityEngine;
using System.Linq; //used for dictionarys



public class TerrainGridHandler : MonoBehaviour
{

    public GameObject terrainParent; // terrain helper object
    
    public int cellsVertical; // how many rows

    public int cellsHorizontal; // how many columns

    public GameObject debugMarker; //tile object
    
    public Dictionary<Vector2Int, GameObject> mapTiles = new Dictionary<Vector2Int, GameObject>(); //dictionary data structure for game tiles 

    private float prefabScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        DontDestroyOnLoad(this);
        //generateGrid(transform.InverseTransformPoint(transform.position), cellsVertical, cellsHorizontal, 1); //use inversetransformpoint to convert from global to local position
        //calculateNeighbors();
    }

    
    
    [ContextMenu("Generate map")]private void EditorMapGen()
    {   
        prefabScale = debugMarker.transform.localScale.x;
        generateGrid(transform.InverseTransformPoint(transform.position), cellsVertical, cellsHorizontal, prefabScale);
        calculateNeighbors();
    }
    
    

    [ContextMenu("Destroy Tiles")]private void DestroyTiles()
    {       
            foreach (KeyValuePair<Vector2Int, GameObject> kvp in mapTiles)
            {
                GameObject.Destroy(kvp.Value);
            }
            mapTiles.Clear();
    }
    
    
    //generate grid and spawn tiles
    private void generateGrid(Vector3 origin, int countVertical, int countHorizontal, float cellGap)
    {
        for (int i = Vector2Int.RoundToInt(origin).x; i < countVertical; i++) // use vector2int for accurate grid coordinates (no decimals)
        {
            for (int j = Vector2Int.RoundToInt(origin).y; j < countHorizontal; j++)
            {

                var newPosition = new Vector3(i * cellGap, 0, j * cellGap); 
                var newTile =  Instantiate(debugMarker, newPosition, Quaternion.Euler(0,0,0)); 

                var tileComponent = newTile.GetComponent<gameTile>();

                tileComponent.transform.SetParent(this.transform); // atttach tile to terrain object
                tileComponent.gridPosition = new Vector2Int(i, j); // assign a 2d-coordinate to the tile
                tileComponent.tileType = tileManager.TileType.Wetland; //assign default tile type
                mapTiles.Add(tileComponent.gridPosition, newTile); // add the newly created tile to a dictionary

            }
        }
    }

   
    
    private void calculateNeighbors()
    {
        for (int i = 0; i < mapTiles.Count; i++) //loop over every tile in the dictionary
        {
            var dictElement = mapTiles.ElementAt(i); //get key-value pair element from the index
            //Debug.Log(dictElement.ToString());

            Vector2Int tilePos = dictElement.Key;

            var tile = dictElement.Value.GetComponent<gameTile>(); //get tile component itself


            //check each 4 side by incrementing x/y by +-1. Do not if the tile is on the edge of the map.
          
            //check left neighbor
            if (tilePos.x != 0)
            {
                GameObject result;
                mapTiles.TryGetValue(new Vector2Int(tilePos.x - 1, tilePos.y), out result);
                if (result != null)
                {
                    tile.adjacentTiles.Add(result);
                   
                }
            }
           
           
            //check right neighbor
            if (tilePos.x != cellsHorizontal-1)
            {
                GameObject result;
                mapTiles.TryGetValue(new Vector2Int(tilePos.x + 1, tilePos.y), out result);
                if (result != null)
                {
                    tile.adjacentTiles.Add(result);
                }
            }

            //check bottom neighbor
            if (tilePos.y != 0)
            {
                GameObject result;
                mapTiles.TryGetValue(new Vector2Int(tilePos.x, tilePos.y - 1), out result);
                if (result != null)
                {
                    tile.adjacentTiles.Add(result);
                }
            }
            

            //check top neighbor
            if (tilePos.y != cellsHorizontal-1)
            {
                GameObject result;
                mapTiles.TryGetValue(new Vector2Int(tilePos.x, tilePos.y + 1), out result);
                if (result != null)
                {
                    tile.adjacentTiles.Add(result);
                }
            }
        }

    }
   
    
}
