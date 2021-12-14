using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSphere : MonoBehaviour
{
    public GameObject newSpherePlanetRef;

    GameObject newSphere;
    bool placingSphere = false;
    public void CreateNewSphere(GameObject obj, Vector3 pos)
    {
        newSphere = Instantiate(obj, pos, Quaternion.Euler(new Vector3(0,0,0)),newSpherePlanetRef.transform);
        placingSphere = true;
    }
    public void CreateNewSphere(GameObject obj)
    {
        CreateNewSphere(obj, Utils.GetMousePositionInWorldSpace());
    }

    private void Update()
    {
        // Lock position to mouse cursor in world space
        if (placingSphere)
        {
            // When user presses M0 lock sphere position
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                placingSphere = false;
            }
            // newSphere.transform.position = Utils.GetMousePositionInWorldSpace();
            newSphere.transform.position = new Vector3(Utils.GetMousePositionInWorldSpace().x, 0, Utils.GetMousePositionInWorldSpace().z);
            // Display a placing grid to make it look better please.
        }
        // Display mini-menu that lets user enter in start velocity, mass and surface gravity
        // Update sphere to match entered values
    }
}
