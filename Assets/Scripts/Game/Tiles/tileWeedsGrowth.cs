using UnityEngine;
using UnityEngine.Events;

public class tileWeedsGrowth : MonoBehaviour
{
    public gameTile tile;

    public int growStage;
    private GameObject currentGrowth;
    public GameObject growStage1;
    public GameObject growStage2;
    public GameObject growStage3;

    private TurnManager tm;
    
    void Start()
    {
        growStage = 0;
        tm = TurnManager.Instance;
        tm.onTurnChanged.AddListener(GrowWeeds);
        tile = transform.GetComponent<gameTile>();
    }
    
    private void GrowWeeds(int random)
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue > 0.80f)
        {
            growStage++;
            SpawnWeedObject();
        }
    }

    private void SpawnWeedObject()
    {
        switch (growStage)
        {
            case 0:
                break;
            case 1:
                currentGrowth = Instantiate(growStage1, tile.transform.position, Quaternion.identity);
                break;
            case 2:
                Destroy(currentGrowth);
                currentGrowth = Instantiate(growStage2, tile.transform.position, Quaternion.identity);
                break;
            case 3:
                Destroy(currentGrowth);
                currentGrowth = Instantiate(growStage3, tile.transform.position, Quaternion.identity);
                break;
        }
    }
        
}
