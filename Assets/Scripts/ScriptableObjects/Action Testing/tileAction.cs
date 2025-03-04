using UnityEngine;

public abstract class tileAction : ScriptableObject
{
    public string actionName;
    public string actionDebugMessage;
    public abstract void affectTile(gameTile tile);
}
