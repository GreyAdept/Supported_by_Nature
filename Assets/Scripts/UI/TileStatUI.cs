using TMPro;
using UnityEngine;

public class TileStatUI : MonoBehaviour
{
    private tileManager tm;
    public TextMeshProUGUI text;
    
    
    void Start()
    {
        tm = tileManager.Instance;
    }

    void Update()
    {   /*
        if (tm.selectedTile != null && tm.selectedTile.plants.Count > 0)
        {
            text.text = "Current tile\n\nType: " + tm.selectedTile.tileType.ToString() + "\n\nPlants: " + tm.selectedTile.plants.Count.ToString();
        }
        else if (tm.selectedTile != null)
        {
            text.text = "Current tile\n\nType: " + tm.selectedTile.tileType.ToString() + "\n\nPlants: " + "0";
        }
        */
    }
}
