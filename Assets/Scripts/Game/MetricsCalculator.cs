using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsCalculator : MonoBehaviour
{

    private TurnManager tm;
    public TerrainGridHandler gridObj;
    private int tileCount;
    public float statOvergrown;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileCount = gridObj.mapTiles.Count;
        tm = TurnManager.Instance;
        tm.onTurnChanged.AddListener(CalculateOvergrownStat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateOvergrownStat(int random)
    {   
        Debug.Log(gridObj.mapTiles.Count);
        foreach (KeyValuePair<Vector2Int, GameObject> kvp in gridObj.mapTiles)
        {
            var tile = kvp.Value.GetComponent<gameTile>();
            //statOvergrown = tile.overgrownState;
            statOvergrown++;
        }
        
    }
}
