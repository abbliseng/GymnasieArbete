using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlacing : MonoBehaviour
{
    public static bool placing = false;
    
    public float pokeForce;
    [Header("Placing stuff")]
    public GameObject sphere;
    public GameObject currentlyPlacing = null;
    public float ScrollSensitvity = 2f;
    public Camera cam;
    public GameObject parent;

    private float raycastMaxDistance = 100000.0f;
    [Header("Planet Preset Stuff")]
    public GameObject basic;
    public ColourSettings colourSettings;
    public ShapeSettings shapeSettings;

    public GameObject PlacePlanet(Vector3 point, Quaternion quar, Transform p)
    {
        // Create basic
        GameObject newPlanet = Instantiate(basic, new Vector3(0,0,0), quar, p);
        Transform model = newPlanet.transform.GetChild(0);
        // Add celestial body script
        model.gameObject.AddComponent<Planet>();
        Planet planet = model.gameObject.GetComponent<Planet>();
        planet.shapeSettings = shapeSettings;
        planet.colourSettings = colourSettings;
        planet.resolution = 256;

        planet.GeneratePlanet();

        newPlanet.transform.position = point;

        return newPlanet;
    }

    public void Place()
    {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("RaycastHitPlane");

        placing = !placing;
        if (placing && Physics.Raycast(ray, out hit, raycastMaxDistance, mask))
        {
            Debug.Log("HIT");
            if (hit.transform.gameObject.tag == "PlacingPlane")
            {
                //Debug.Log("Instantiating new sphere to place");
                // currentlyPlacing = Instantiate(sphere, hit.point, Quaternion.identity, parent.transform);
                GlobalVars.Pause();
                currentlyPlacing = PlacePlanet(hit.point, Quaternion.identity, parent.transform);
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
            // GlobalVars.Resume();
        }
    }

    private void Update()
    {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("RaycastHitPlane");

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (placing && currentlyPlacing != null)
            {
                if (Physics.Raycast(ray, out hit, raycastMaxDistance, mask))
                {
                    currentlyPlacing.transform.position = new Vector3(hit.point.x, currentlyPlacing.transform.position.y, hit.point.z);
                    if (currentlyPlacing.transform.position.y != 0)
                    {
                        LineRenderer lr;
                        if (currentlyPlacing.GetComponent<LineRenderer>() == null)
                        {
                            currentlyPlacing.AddComponent<LineRenderer>();
                        }
                        lr = currentlyPlacing.GetComponent<LineRenderer>();
                        lr.startWidth = 1f;
                        lr.endWidth = 1f;
                        lr.startColor = Color.white;
                        lr.SetPositions(new[] { currentlyPlacing.transform.position, hit.point });
                    }
                }
                if (Input.GetAxis("Mouse ScrollWheel") != 0f && Input.GetKey(KeyCode.LeftControl))
                {
                    float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
                    currentlyPlacing.transform.position += new Vector3(0, ScrollAmount, 0);
                }
            }
            
            // Lock the sphere's position when lmb is pressed.
            if (Input.GetMouseButtonDown(0) && placing)
            {
                Destroy(currentlyPlacing.GetComponent<LineRenderer>());

                placing = false;
                currentlyPlacing = null;
                // GlobalVars.Resume();
            }
        } else
        {
            placing = false;
            if (currentlyPlacing != null)
            {
                Destroy(currentlyPlacing);
            }
            currentlyPlacing = null;
            // GlobalVars.Resume();
        }
    }
}
