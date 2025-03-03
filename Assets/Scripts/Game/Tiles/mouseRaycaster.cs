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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = tileManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue(); //read mouse position
        worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 18f)); //convert mouse position into world position
        projectedPos = Vector3.ProjectOnPlane(worldPos, new Vector3(0, 1, 0)); //account for camera rotation

        //Touch touch = Input.GetTouch(0);
        //if (Input.GetMouseButton(0) || touch.phase == UnityEngine.TouchPhase.Began)
        
        GameObject selectedTile = CheckTileHitting();
        if (selectedTile != null)
        {
            tm.selectedTile = selectedTile.GetComponent<gameTile>(); //send selected tile to tilemanager instnace
            selectedTile.GetComponent<gameTile>().isHovered = true; 
            //selectedTile.GetComponent<gameTile>().clickHandler();
        }
        else
        {
            tm.selectedTile = null; //if no tile hovered, select nothing
        }
    }

    public GameObject CheckTileHitting()
    {
        RaycastHit hit;
        
        Physics.Raycast(new Vector3(projectedPos.x, projectedPos.y + 1, projectedPos.z), new Vector3(0, -1, 0), out hit); //fire ray directly above tilemap
        Debug.Log(hit.ToString());
        if (hit.collider.gameObject != null && hit.collider.CompareTag("Tile")) //check if ray hits a tile
        {
            return hit.collider.gameObject;
           
            //var tileObj = hit.collider.gameObject.GetComponent<gameTile>(); //get tile component itself
        }
        else
        {
            return null;
        }
        
        
x
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(projectedPos, 0.5f);
    }
}
