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

    public void doEffect(gameTile tile)
    {
        
    }


}
