using System.Collections.Generic;
using UnityEngine;

public class TerrainGridHandler : MonoBehaviour
{

    public GameObject terrainParent;
    
    public int cellsVertical;

    public int cellsHorizontal;

    public GameObject debugMarker; //tile object
    
    public Dictionary<Vector2Int, GameObject> mapTiles = new Dictionary<Vector2Int, GameObject>(); //dictionary data structure for game tiles 

    //private List<Vector3> cellsCoordinates = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        generateGrid(transform.InverseTransformPoint(transform.position), cellsVertical, cellsHorizontal, 1);
        
    }
    
    //generate grid and spawn tiles
    private void generateGrid(Vector3 origin, int countVertical, int countHorizontal, int cellGap)
    {
        for (int i = Vector2Int.RoundToInt(origin).x; i < countVertical; i++)
        {
            for (int j = Vector2Int.RoundToInt(origin).y; j < countHorizontal; j++)
            {
                var newPosition = new Vector3(i * cellGap, 0, j * cellGap);
                var newTile =  Instantiate(debugMarker, newPosition, Quaternion.Euler(90,0,0));
                var tileComponent = newTile.GetComponent<gameTile>();
                tileComponent.transform.SetParent(this.transform);
                tileComponent.gridPosition = new Vector2Int(i, j);
                mapTiles.Add(tileComponent.gridPosition, newTile);

            }
        }
    }

   
    
}
