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
    
    public Rigidbody rb;
    public Transform tf;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        velocity = initialVelocity;
        RecalculateMass();
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);

    }

    void OnValidate () {
        RecalculateMass();
        gameObject.name = bodyName;
    }

    public void RecalculateMass () {
        tf.localScale = new Vector3(radius,radius,radius);
        mass = surfaceGravity * radius * radius / GlobalVars.gravitationalConstant;
        rb.mass = mass;
    }
}
