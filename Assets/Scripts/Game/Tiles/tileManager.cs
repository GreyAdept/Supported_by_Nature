using UnityEngine;
using Utils;

public class tileManager : SingletonMonoBehaviour<tileManager>
{

    public enum TileType
    {
        Water,
        Wetland,
        Forest
    };
    
    public Material materialWetland;
    public Material materialWater;
    public Material materialForest;
    
    public gameTile selectedTile;

    public WetlandAction[] actionsWetland;

    public tileAction tileAction;
    public tileAction removePlantAction;

    public bool toolBeingUsed = false;



    //overall stats
    public int waterHealth;


    void Start()
    {
        Instantiate(materialWetland);
        Instantiate(materialWater);
        Instantiate(materialForest);
    }
}
