using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerRaycaster : MonoBehaviour
{

    public Vector3 worldPos;
    [SerializeField] private Vector3 projectedPos;
    public Camera cam;
    public GameObject tileHoverOver;

    
    [SerializeField] private GameObject selectedTile;


    private InputManager inputManager;

   
    public bool isTouching;
    

    public GameObject tileIndicator;
  


    private void Start()
    {
        
        //inputManager = InputManager.Instance;
    }

 
    private void Update()
    {
      
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(inputManager.pointerPosition);
        if (plane.Raycast(ray, out float distance))
        {
            worldPos = ray.GetPoint(distance);
            projectedPos = worldPos;
        }
        
        var newSelectedTile = CheckTileHitting();
        
        if (selectedTile != null && newSelectedTile != selectedTile)
        {
           
            newSelectedTile.GetComponent<gameTile>().StartHover();
            //tm.selectedTile = newSelectedTile.GetComponent<gameTile>(); //send selected tile to tilemanager instnace
            selectedTile.GetComponent<gameTile>().ClearHover();
        }

        selectedTile = newSelectedTile;

        /*
        if (tm.toolBeingUsed)
        {
            ToggleIndicatorVisibility(1);
        }
        else
        {
            ToggleIndicatorVisibility(0);
        }
        */

        SnapToGrid();


    }
    

    public GameObject CheckTileHitting()
    {
        RaycastHit hit;
        
        Physics.Raycast(new Vector3(projectedPos.x, projectedPos.y+1, projectedPos.z), new Vector3(0, projectedPos.y-1, 0).normalized, out hit); //fire ray directly above tilemap
        UnityEngine.Debug.DrawLine(new Vector3(projectedPos.x, projectedPos.y + 1, projectedPos.z), worldPos);
        
        if (hit.collider != null && hit.collider.CompareTag("Tile")) //check if ray hits a tile
        {
           
            return hit.collider.gameObject;
        }
        else if (hit.collider != null && hit.collider.CompareTag("NPC"))
        {
            DialogueManager.instance.InteractWithNPC();
            return selectedTile;
        }
        
        return selectedTile;

        
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(worldPos, 0.5f);
    }

 

    private void SnapToGrid()
    {   
        //allign tile indicator to selected tile on the grid
        /*
        if(tm.selectedTile != null && tm)
        {
            tileIndicator.transform.position = new Vector3(tm.selectedTile.gameObject.transform.position.x, 0.32f, tm.selectedTile.gameObject.transform.position.z);
        }
        */
       
    }

    public void ToggleIndicatorVisibility(int toggle)
    {   
       if (toggle == 0)
        {
            tileIndicator.gameObject.SetActive(false);
        }
       else if (toggle == 1)
        {
            tileIndicator.gameObject.SetActive(true);
        }
    }
}
