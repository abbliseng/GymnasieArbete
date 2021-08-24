using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestialbody : MonoBehaviour
{

    public GameObject[] celestialbodies;
    private float gravityConstant = (float)6.67 * Mathf.Pow(10, -11);
    void Start()
    {
        // On start always reset the celestialbody array, loop through and remove self(?) Maybe don't remove self but always just neglect it.
        // If only the array has changed?
        if (celestialbodies != GameObject.FindGameObjectsWithTag("GravityEffected"))
        {
            celestialbodies = GameObject.FindGameObjectsWithTag("GravityEffected");
            foreach (GameObject body in celestialbodies)
            {
                if (body == this.gameObject)
                {
                    // Debug.Log("Found self");
                }
            }
        }
        // foreach (GameObject body in celestialbodies){
        //     Debug.Log(body);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject body in celestialbodies)
        {
            if (body != this.gameObject)
            {
                Vector3 difference = this.transform.position - body.transform.position;
                float dist = difference.magnitude;
                Vector3 gravityDirection = difference.normalized;
                float gravity = gravityConstant * (this.GetComponent<Rigidbody>().mass * body.GetComponent<Rigidbody>().mass)/(dist*dist);
                Vector3 gravityVector = (gravityDirection * gravity);
                body.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
            }
        }
    }
}
