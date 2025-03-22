using UnityEngine;

public class tileWeedsGrowth : MonoBehaviour
{
    public gameTile tile;

    private TurnManager tm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = TurnManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
