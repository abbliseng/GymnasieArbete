using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlacing : MonoBehaviour
{
    
    public float pokeForce;
    [Header("Placing stuff")]
    public GameObject sphere;
    public bool placing = false;
    public GameObject currentlyPlacing = null;
    public float ScrollSensitvity = 2f;
    public Camera cam;

    private float raycastMaxDistance = 100000.0f;

    private void Update()
    {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("RaycastHitPlane");
       
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                placing = !placing;
                if (placing && Physics.Raycast(ray, out hit, raycastMaxDistance, mask))
                {
                    //Debug.Log("HIT");
                    if (hit.transform.gameObject.tag == "PlacingPlane")
                    {
                        //Debug.Log("Instantiating new sphere to place");
                        currentlyPlacing = Instantiate(sphere, hit.point, Quaternion.identity);
                    }
                } 
                else if (placing)
                {
                    //Debug.Log("Couldn't find hit");
                    placing = !placing;
                }
                else if (!placing)
                {
                    if (currentlyPlacing != null)
                    {
                        Destroy(currentlyPlacing);
                    }
                    currentlyPlacing = null;
                }
            } else
            {
                if (placing && currentlyPlacing != null)
                {
                    if (Physics.Raycast(ray, out hit, raycastMaxDistance, mask))
                    {
                        currentlyPlacing.transform.position = new Vector3(hit.point.x, currentlyPlacing.transform.position.y, hit.point.z);
                    }
                    if (Input.GetAxis("Mouse ScrollWheel") != 0f && Input.GetKey(KeyCode.LeftControl))
                    {
                        float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
                        //Debug.Log(ScrollAmount);
                        currentlyPlacing.transform.position += new Vector3(0, ScrollAmount, 0);
                    }
                }
            }
            // Lock the sphere's position when lmb is pressed.
            if (Input.GetMouseButtonDown(0) && placing)
            {
                placing = false;
                currentlyPlacing = null;
            }
        } else
        {
            placing = false;
            if (currentlyPlacing != null)
            {
                Destroy(currentlyPlacing);
            }
            currentlyPlacing = null;
        }
    }
}
