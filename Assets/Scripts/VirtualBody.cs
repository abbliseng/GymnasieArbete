using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private int i = 0;
    private int k = 31;

    public VirtualBody(float radius, Vector3 velocity, float mass) {
        this.radius = radius;
        this.velocity = velocity;
        this.mass = mass;
    }

    void Awake () {
        points = new Vector3[DrawOrbits.steps];
        i = 0;
        velocity = initialVelocity;
        rb = GetComponent<Rigidbody> ();

        //TODO Get and update all planet info from parent.
    }

    public void UpdateVelocity (Vector3 acceleration, float timeStep) {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep) {
        rb.MovePosition (rb.position + velocity * timeStep);
        if (i == DrawOrbits.steps){
            GlobalVars.timeScale = 1f;
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
        // } else if (k > 30) {
        //     points.RemoveAt(0);
        //     points[DrawOrbits.steps-1] = this.transform.position - this.transform.parent.position;
        // }
        k++;
    }

}
