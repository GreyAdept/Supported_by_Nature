using Utils;

public class tileManager : SingletonMonoBehaviour<tileManager>
{

    public enum TileType
    {
        Water,
        Wetland,
        Forest
    };

    public WetlandAction[] actionsWetland;

}
