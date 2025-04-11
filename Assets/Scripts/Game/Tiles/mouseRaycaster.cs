using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEditor.DeviceSimulation.TouchPhase;

public class mouseRaycaster : MonoBehaviour
{

    [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector3 worldPos;
    [SerializeField] private Vector3 projectedPos;
    public Camera cam;
    public GameObject tileHoverOver;

    private tileManager tm;
    private GameObject selectedTile;

    private InputSystem_Actions inputActions;

    //touch screen input
    private bool isTouching;
    [SerializeField] private Vector2 touchPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = tileManager.Instance;
        
    }


    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.Touch.Touch.performed += ctx => touchPosition = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue(); //read mouse position
        /* // this is the old raycast script (for top down camera)
        worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Vector3.Distance(GameObject.Find("TerrainGrid").transform.position, cam.transform.position))); //convert mouse position into world position
        projectedPos = Vector3.ProjectOnPlane(worldPos, new Vector3(0, 1, 0)); //account for camera rotation
        //projectedPos = worldPos;
        */
        
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (plane.Raycast(ray, out float distance))
        {
            worldPos = ray.GetPoint(distance);
            projectedPos = worldPos;
        }
        
        var newSelectedTile = CheckTileHitting();
        /*
        if (tm.toolBeingUsed)
        {   
            var newSelectedTile = CheckTileHitting();
        }
        */
        
        if (selectedTile != null && newSelectedTile != selectedTile)
        {
            newSelectedTile.GetComponent<gameTile>().StartHover();
            tm.selectedTile = newSelectedTile.GetComponent<gameTile>(); //send selected tile to tilemanager instnace
            selectedTile.GetComponent<gameTile>().ClearHover();
        }

        selectedTile = newSelectedTile;
       
    }   


    public GameObject CheckTileHitting()
    {
        RaycastHit hit;
        
        Physics.Raycast(new Vector3(projectedPos.x, projectedPos.y+1, projectedPos.z), new Vector3(0, projectedPos.y-1, 0).normalized, out hit); //fire ray directly above tilemap
  
        if (hit.collider != null && hit.collider.CompareTag("Tile")) //check if ray hits a tile
        {
            return hit.collider.gameObject;
        }
        else
        {
            return selectedTile;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(worldPos, 0.5f);
    }

    private IEnumerator ClearHoverHelper()
    {
        yield return new WaitForSeconds(1);
    }
}
