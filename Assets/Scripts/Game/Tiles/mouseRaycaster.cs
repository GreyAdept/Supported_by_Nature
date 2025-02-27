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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            selectedTile.GetComponent<gameTile>().isHovered = true;
            //selectedTile.GetComponent<gameTile>().clickHandler();
        }
    }

    public GameObject CheckTileHitting()
    {
        RaycastHit hit;
        
        Physics.Raycast(new Vector3(projectedPos.x, projectedPos.y + 1, projectedPos.z), new Vector3(0, -1, 0), out hit); //fire ray directly above tilemap
        Debug.Log(hit.ToString());
        if (hit.collider.CompareTag("Tile")) //check if ray hits a tile
        {
            return hit.collider.gameObject;
            
            //var tileObj = hit.collider.gameObject.GetComponent<gameTile>(); //get tile component itself
        }
        else
        {
            return null;
        }
        
        

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(projectedPos, 0.5f);
    }
}
