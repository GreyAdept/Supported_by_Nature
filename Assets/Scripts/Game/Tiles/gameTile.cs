using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class gameTile : MonoBehaviour
{
    //grid related
    public List<GameObject> adjacentTiles = new List<GameObject>();
    public Vector2Int gridPosition;

    //tile status
    public bool isSelected = false;
    public bool isHovered = false;

    //components
    private tileManager tileManager;
    private Renderer rend;
    private SpriteRenderer sRend;
    private Color defaultColor;
    
    //tile materials
    private tileSelectedEffect effectHandler;
    
    
    // tile data
    public tileManager.TileType tileType;
    public bool isNextToLand;
   
    //plant related
    public int overgrownState; // how much have the reeds grown
    public Plant grownPlant; //plant that the player can place
    private GameObject plantPrefab;
    public int plantGrowStage;

    //placement indicator
    public GameObject placementIndicatorPrefab;
    private GameObject indicator;


    private void Awake()
    {
        /*
        ActionButton.OnPlayerStateChanged += (InputManager.PlayerState context) =>
        {
            if (context == InputManager.PlayerState.placement)
            {
                SetIndicatorColor();
                SetIndicatorOn();
            }
            else
            {
                SetIndicatorColor();
                SetIndicatorOff();
            }

        };
        */

        ActionButton.OnPlayerStateChanged += PlayerStateStateChangedHandler;

        //TurnManager.OnTurnChanged += () => SetIndicatorColor();

        TurnManager.OnTurnChanged += TurnChangedHandler;



    }


    void Start()
    {
        isNextToLand = false;
        grownPlant = null;
        placementIndicatorPrefab = InputManager.Instance.placementIndicator;
        indicator = Instantiate(placementIndicatorPrefab, new Vector3(transform.position.x, 0.32f, transform.position.z), placementIndicatorPrefab.transform.rotation);
        indicator.gameObject.SetActive(false);
        tileManager = tileManager.Instance;
        sRend = indicator.GetComponent<SpriteRenderer>();
        defaultColor = sRend.color;

        TurnManager.Instance.onTurnChanged.AddListener(IncrementPlantGrowStage);
         

        foreach (GameObject t in adjacentTiles) //<-- check if tile is next to land
        {
            if (t.GetComponent<gameTile>().tileType == tileManager.TileType.Wetland)
            {
                isNextToLand = true;
            }
        }
    }


    private void PlayerStateStateChangedHandler(InputManager.PlayerState context)
    {
        if (context == InputManager.PlayerState.placement)
        {
            SetIndicatorColor();
            SetIndicatorOn();
        }
        else
        {
            SetIndicatorColor();
            SetIndicatorOff();
        }
    }

    private void TurnChangedHandler()
    {
        SetIndicatorColor();
    }


    private void OnDisable()
    {
        TurnManager.OnTurnChanged -= TurnChangedHandler;
        ActionButton.OnPlayerStateChanged -= PlayerStateStateChangedHandler;

    }

    private void SetIndicatorOn()
    {
        indicator.gameObject.SetActive(true);
    }

    private void SetIndicatorOff()
    {
        indicator.gameObject.SetActive(false);
    }

    private void SetIndicatorToYellow()
    {
        sRend.color = Color.yellow;
    }

    private void SetIndicatorToWhite()
    {
        sRend.color = defaultColor;
    }

    private void SetIndicatorToRed()
    {
        sRend.color = Color.red;
    }
    
    public string ReturnTileData()
    {
        return gridPosition.ToString();
    }


    public void ClearHover()
    {
        //effectHandler.StopParticle();
        isHovered = false;
    }

    public void StartHover()
    {
        //effectHandler.PlayParticle(); it was replaced by tile selector sprite
    }

    public void IncrementPlantGrowStage(int random)
    {
        if (grownPlant && grownPlant.plantGrowStage < 3 && overgrownState < 3)
        {
            grownPlant.plantGrowStage++;
        }

        UpdatePlant();
    }

    public void UpdatePlant() //Update the plant object accordingly with a different model as it grows bigger
    {   
        if (grownPlant)
        {
            switch (grownPlant.plantGrowStage)
            {
                case 0:
                    plantPrefab = Instantiate(grownPlant.organismPrefab, this.transform.position, grownPlant.organismPrefab.transform.rotation);
                    break;
                case 1:
                    Destroy(plantPrefab);
                    plantPrefab = Instantiate(grownPlant.organismPrefab, this.transform.position, grownPlant.organismPrefab.transform.rotation);
                    break;
                case 2:
                    Destroy(plantPrefab);
                    plantPrefab = Instantiate(grownPlant.plantGrowStagePrefab2, this.transform.position, grownPlant.organismPrefab.transform.rotation);
                    break;
                case 3:
                    Destroy(plantPrefab);
                    plantPrefab = Instantiate(grownPlant.plantGrowStagePrefab3, this.transform.position, grownPlant.organismPrefab.transform.rotation);
                    break;
               
            }

            plantGrowStage = grownPlant.plantGrowStage;

        }
    }

    private void SetIndicatorColor()
    {
        if (GetComponent<tileWeedsGrowth>().growStage == 2)
        {   
            SetIndicatorToYellow();
        }
        else if (GetComponent<tileWeedsGrowth>().growStage >= 3)
        {
            SetIndicatorToRed();
        }
        else
        {
            SetIndicatorToWhite();
        }
    }


   
}
