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
    
    public gameTile selectedTile;

    public WetlandAction[] actionsWetland;

    public tileAction tileAction;
    public tileAction removePlantAction;
    
    void Update()
    {   
        /*
        if (Input.GetMouseButtonDown(0) && selectedTile != null)
        {
            tileAction.affectTile(selectedTile);
        }
        if (Input.GetMouseButtonDown(1) && selectedTile != null)
        {
            removePlantAction.affectTile(selectedTile);
        }
        */
    }
    
    


}
