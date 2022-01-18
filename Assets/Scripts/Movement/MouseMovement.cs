using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float dragSpeed = 20;
    private Vector3 dragOrigin;

    private float speedMod = 10.0f;//a speed modifier
    private Vector3 point;//the coord to the point where the camera looks at

    void Update()
    {
        // Raycast setup
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("RaycastHitPlane");
        
        // Drag camera movement
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            transform.Translate(move, Space.World);
        }

        // Rotate around point
        if (Input.GetMouseButtonDown(2) && Physics.Raycast(ray, out hit, 1000.0f, mask))
        {
            if (hit.transform.gameObject.tag == "PlacingPlane")
            {
                point = hit.point;//get target's coords
                transform.LookAt(point);//makes the camera look to it
            }
        }
        if (Input.GetMouseButton(2))
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        }
    }


}