using UnityEngine;

[CreateAssetMenu(fileName = "TileAction", menuName = "TileAction")]
public class sampleAction : tileAction
{
    public override void affectTile(gameTile tile)
    {
        Debug.Log(actionDebugMessage);
        
    }
}
