using UnityEngine;

public abstract class organism : ScriptableObject
{
    public string organismName;
    
    // scrapped feature
    //public float organismGrowFactor;
    //public bool isInvasive
        
    
    public GameObject organismPrefab; 

    public abstract void OrganismBehavior();
}
