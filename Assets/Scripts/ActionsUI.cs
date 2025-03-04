using UnityEngine;

public class ActionsUI : MonoBehaviour
{

    public GameObject actionUIForest;

    public GameObject actionUIWetland;

    public GameObject actionUIWater;

    private tileManager tm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = tileManager.Instance;
        actionUIForest.SetActive(false);
        actionUIWetland.SetActive(false);
        actionUIWater.SetActive(false);
    }

 
    void LateUpdate()
    {
        if (tm.selectedTile != null)
        {
            ChangeUI(tm.selectedTile);
        }
        
    }

    public void ChangeUI(gameTile tile)
    {
        var tileType = tile.tileType;
        switch (tileType)
        {
            case tileManager.TileType.Forest:
                actionUIForest.SetActive(true);
                actionUIWetland.SetActive(false);
                actionUIWater.SetActive(false);
                break;
            case tileManager.TileType.Wetland:
                actionUIForest.SetActive(false);
                actionUIWetland.SetActive(true);
                actionUIWater.SetActive(false);
                break;
            case tileManager.TileType.Water:
                actionUIForest.SetActive(false);
                actionUIWetland.SetActive(false);
                actionUIWater.SetActive(true);
                break;
        }
    }
}
