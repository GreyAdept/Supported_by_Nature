using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTile : MonoBehaviour
{
   
    public List<GameObject> adjacentTiles = new List<GameObject>();
    public Vector2Int gridPosition;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void returnColor()
    {
        StartCoroutine(returnColorHelper());
    }

    public IEnumerator returnColorHelper()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material.color = Color.white;
    }
    
}
