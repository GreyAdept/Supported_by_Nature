using UnityEngine;

[CreateAssetMenu(fileName = "TileAction", menuName = "TileAction")]
public class sampleAction : tileAction
{
    public override void affectTile(tileManager tm)
    {
        Debug.Log(actionDebugMessage);
    }
}
