using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Celestialbody : MonoBehaviour
{
    public GameObject[] celestialbodies;
    private float gravityConstant = (float)6.67 * Mathf.Pow(10, -11);

    private void Awake() {
        int i = 0;
        celestialbodies = new GameObject[GameObject.FindGameObjectsWithTag("GravityEffected").Length - 1];
        foreach (GameObject body in GameObject.FindGameObjectsWithTag("GravityEffected")) {
            if (body != this.gameObject) {
                celestialbodies[i] = body;
                i++;
            }
        }
    }

    private void Start() {
        // GetComponent<Rigidbody>().velocity = new Vector3(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject body in celestialbodies)
        {
            Vector3 difference = this.transform.position - body.transform.position;
            // Debug.Log(difference);
            float dist = difference.magnitude;
            // Debug.Log(dist);
            Vector3 gravityDirection = difference.normalized;
            // Debug.Log(gravityDirection);
            float gravity = gravityConstant * (this.GetComponent<Rigidbody>().mass * body.GetComponent<Rigidbody>().mass)/(dist*dist);
            // Debug.Log(gravity);
            Vector3 gravityVector = (gravityDirection * gravity);
            // Debug.Log(gravityVector);
            body.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
            
        }
    }
}
