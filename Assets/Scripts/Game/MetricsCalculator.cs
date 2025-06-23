using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsCalculator : MonoBehaviour
{   
    public gameTile tile;
    public tileWeedsGrowth tileWeeds;

    public int tileBiodiversity;


    void Start()
    {   
        tile = GetComponent<gameTile>();
        tileWeeds = GetComponent<tileWeedsGrowth>();
    }

    public void CalculateBiodiversity()
    {
        

    }

    public void UpdateCurrentBiodiversity() //this is called by a broadcasted message
    {
        CalculateBiodiversity();
        //Debug.Log("Message received", this);
    }
}

  
