using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : MonoBehaviour
{
    
    public float surfaceGravity;
    public Vector3 initialVelocity;
    public string bodyName = "Unnamed";
    public float radius;

    public Vector3 velocity { get; private set; }
    public float mass { get; private set; }
    // // private double G = 6.67408 * Mathf.Pow(10,-11); // Should be a const
    // public static List<CelestialBody> Attractors;
    
    public Rigidbody rb;
    public Transform tf;

    void Awake () {
        rb = GetComponent<Rigidbody> ();
        // tf = GetComponent<Transform> (); //This gets the wrong component, should get childs transform
        velocity = initialVelocity;
        RecalculateMass ();
    }

    private void Start() {
        if (GameObject.Find("Virtual "+gameObject.name) == null) {
            Debug.Log("No : " + gameObject.name);
            // Create a virtualcopy
            // DrawOrbits.CreateVirtualCopy(this);
        }
        // } else {
            // FindObjectOfType<DrawOrbits>().CreateVirtualCopy(this);
            // FindObjectOfType<DrawOrbits>().UpdateVirtualCopy(this, GameObject.Find("Virtual "+gameObject.name).GetComponent<VirtualBody>());
        // }
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);

    }

    void OnValidate () {
        RecalculateMass ();
        gameObject.name = bodyName;
    }

    public void RecalculateMass () {
        tf.localScale = new Vector3(radius,radius,radius);
        mass = surfaceGravity * radius * radius / GlobalVars.gravitationalConstant;
        rb.mass = mass;
    }
}
