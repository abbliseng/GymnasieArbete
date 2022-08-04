using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    public Camera cam;
    public float doubleClickResetTime = 1f;
    public bool following = false;

    private float doubleClickTime = 0f;
    private float raycastMaxDistance = 100000.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.detectCollisions = false;
    }

    private void Update()
    {
        doubleClickTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && doubleClickTime > 0f)
        {
            doubleClickTime = 0f;
            RaycastSelect();
        }else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            doubleClickTime = doubleClickResetTime;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            following = false;
            this.transform.SetParent(null);
        }
    }

    void RaycastSelect()
    {
        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("RaycastPlanetPlane");

        if (Physics.Raycast(ray, out hit, raycastMaxDistance, mask))
        {
            Debug.Log(hit.transform.name);
            this.transform.SetParent(hit.transform);
            following = true;
        }
    }
}
