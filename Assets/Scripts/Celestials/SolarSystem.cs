using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly public float G = GlobalVars.gravitationalConstant;
    public GameObject[] celestials { get; private set; }

    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();
    }


    private void FixedUpdate()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        // ResetVelocity();
        // InitialVelocity();
        if (!GlobalVars.paused)
        {
            Gravity();
        }
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            Vector3 force = Vector3.zero;
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    force += (b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r));
             
                    //a.GetComponent<Rigidbody>().AddForce(force);
                }
            }
            UpdatePosition(a,
                CalculateVelocity(
                    a,
                    CalculateAcceleration(
                        force,
                        a.GetComponent<Rigidbody>().mass),
                    a.GetComponent<Rigidbody>().velocity,
                    GlobalVars.physicsTimeStep * GlobalVars.timeScale),
                a.GetComponent<Rigidbody>().position,
                GlobalVars.physicsTimeStep * GlobalVars.timeScale);
        }
    }

    public void InitialVelocity(GameObject a, GameObject b)
    {
        a.GetComponent<Rigidbody>().velocity += b.GetComponent<Rigidbody>().velocity;
        if (!a.Equals(b))
        {
            float m2 = b.GetComponent<Rigidbody>().mass;
            float r = Vector3.Distance(a.transform.position, b.transform.position);
            a.transform.LookAt(b.transform);

            a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
        }   
    }

    public void InitialVelocity(GameObject a)
    {
        foreach (GameObject b in celestials)
        {
            if (!a.Equals(b))
            {
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);
                a.transform.LookAt(b.transform);

                a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
            }
        }   
    }

    public void InitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }

    public void ResetVelocity(GameObject a)
    {
        a.GetComponent<Rigidbody>().velocity = Vector3.zero;       
    }

    public void ResetVelocity()
    {
        foreach (GameObject a in celestials)
        {
            a.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void UpdateCelestials(GameObject newCelestial)
    {
        celestials = celestials;
    }

    public Vector3 CalculateAcceleration(Vector3 force, float mass)
    {
        return force / mass; //f = m * a; a = f/m
    }

    public Vector3 CalculateVelocity(GameObject target, Vector3 acceleration, Vector3 currentVelocity, float timeStep)
    {
        Vector3 velocity = currentVelocity + acceleration * timeStep;
        target.GetComponent<Rigidbody>().velocity = velocity;
        return velocity;
    }

    public void UpdatePosition(GameObject target, Vector3 velocity, Vector3 currentPosition, float timeStep)
    {
        target.GetComponent<Rigidbody>().MovePosition(currentPosition + velocity * timeStep);
    }
}
