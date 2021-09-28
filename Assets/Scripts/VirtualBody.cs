using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class VirtualBody : MonoBehaviour
{
    public float radius;
    public float mass;

    public Rigidbody rb;
    public LineRenderer lr;
    public Vector3[] points;

    public Vector3 initialVelocity;
    public Vector3 velocity;

    public GameObject parent;
    public CelestialBody parentCel;

    private int i = 0;
    private int k = 31;

    public VirtualBody(float radius, Vector3 velocity, float mass, GameObject sender) {
        this.radius = radius;
        this.velocity = velocity;
        this.mass = mass;
        this.parent = sender;
    }

    void Awake () {
        points = new Vector3[DrawOrbits.steps];
        i = 0;
        velocity = initialVelocity;
        rb = GetComponent<Rigidbody> ();

        //TODO Get and update all planet info from parent.
        UpdatePlanetInfo();
    }

    // private void Update() {
    //     UpdatePlanetInfo();
    // }

    private void OnValidate() {
        UpdatePlanetInfo();
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);
        if (i == DrawOrbits.steps){
            GlobalVars.timeScale = 4f;
            GlobalVars.simulateCelestialBodies = true;
            i++;
        }

        if (i < DrawOrbits.steps && k > 30) {
            GlobalVars.simulateCelestialBodies = false;
            GlobalVars.timeScale = 80f;
            points[i] = this.transform.position - this.transform.parent.position;
            lr.positionCount = i + 1;
            lr.SetPositions(points);
            i++;
            k = 0;
        } else if (k > 30) { // TODO Very very very inefficent, plez fix
            
            for (int j = 0; j < DrawOrbits.steps - 1; j++) {
                points[j] = points[j+1];
            }
            points[DrawOrbits.steps - 1] = this.transform.position - this.transform.parent.position;
            lr.SetPositions(points);
            k = 0;
        }
        k++;
    }

    public void UpdatePlanetInfo() {
        mass = parentCel.mass;
        rb.mass = mass;
        radius = parentCel.radius;

        if (parentCel.velocity == new Vector3(0,0,0)){
            initialVelocity = parentCel.initialVelocity;
        } else {
            velocity = parentCel.velocity;
        }
    }

}
