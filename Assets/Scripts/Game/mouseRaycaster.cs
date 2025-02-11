using UnityEngine;
using UnityEngine.InputSystem;

public class mouseRaycaster : MonoBehaviour
{

    [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector3 worldPos;
    [SerializeField] private Vector3 projectedPos;
    public Camera cam;



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

        CheckTileHitting();
       
    }

    void CheckTileHitting()
    {
        RaycastHit hit;
        
        Physics.Raycast(new Vector3(projectedPos.x, projectedPos.y + 1, projectedPos.z), new Vector3(0, -1, 0), out hit); //fire ray directly above tilemap
        Debug.Log(hit.ToString());
        if (hit.collider.CompareTag("Tile")) //check if ray hits a tile
        {
            var tileObj = hit.collider.gameObject.GetComponent<gameTile>(); //get tile component itself
            tileObj.GetComponent<Renderer>().material.color = Color.red;

            tileObj.returnColor();
        }
        

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(projectedPos, 0.5f);
    }
}
