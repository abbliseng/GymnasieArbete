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


    private void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        LayerMask mask = LayerMask.GetMask("RaycastHitPlane");
       
        if (Cursor.lockState == CursorLockMode.Confined)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                placing = !placing;
                if (placing && Physics.Raycast(ray, out hit, 1000.0f, mask))
                {
                    if (hit.transform.gameObject.tag == "PlacingPlane")
                    {
                        currentlyPlacing = Instantiate(sphere, hit.point, Quaternion.identity);
                    }
                } else if (!placing)
                {
                    if (currentlyPlacing != null)
                    {
                        Destroy(currentlyPlacing);
                    }
                    currentlyPlacing = null;
                }
            } else
            {
                if (placing && Physics.Raycast(ray, out hit, 1000.0f, mask) && currentlyPlacing != null)
                {
                    currentlyPlacing.transform.position = hit.point;
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

        /*
        if (Input.GetMouseButtonDown(0))
        {
            
            if (Physics.Raycast(ray, out hit, 1000.0f, mask))
            {
                if (hit.transform.gameObject.tag == "PlacingPlane")
                {
                    Debug.Log("Hit the ground at: " + hit.point);
                    Instantiate(sphere, hit.point, Quaternion.identity);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);
                }
            }
        }
        */

    }
}
