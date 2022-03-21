using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;


    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log("wow");
        Gravity();
    }

    void Gravity()
    {
        foreach(GameObject a in celestials)
        {
            foreach(GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    Vector3 force = (b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r));

                    UpdatePosition(a,
                        CalculateVelocity(
                            CalculateAcceleration(
                                force,
                                a.GetComponent<Rigidbody>().mass),
                            a.GetComponent<Rigidbody>().velocity,
                            GlobalVars.physicsTimeStep * GlobalVars.timeScale),
                        a.GetComponent<Rigidbody>().position,
                        GlobalVars.physicsTimeStep * GlobalVars.timeScale);
                    
                    // a.GetComponent<Rigidbody>().AddForce(force);
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach ( GameObject a in celestials)
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

    public Vector3 CalculateAcceleration(Vector3 force, float mass)
    {
        return force / mass; //f = m * a; a = f/m
    }

    public Vector3 CalculateVelocity(Vector3 acceleration, Vector3 currentVelocity, float timeStep)
    {
        Vector3 velocity = currentVelocity + acceleration * timeStep;
        return velocity;
    }

    public void UpdatePosition(GameObject target, Vector3 velocity, Vector3 currentPosition, float timeStep)
    {
        target.GetComponent<Rigidbody>().MovePosition(currentPosition + velocity * 1000);
    }
}
