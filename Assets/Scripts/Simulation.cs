using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    CelestialBody[] bodies;
    VirtualBody[] vBodies;
    static Simulation instance;
    private float timeElapsed;

    [Range(0,100)]
    public float sped = 1f;

    private void OnValidate() {
        GlobalVars.timeScale = sped;
        Time.timeScale = GlobalVars.timeScale;
    }

    void Awake () {
        // bodies = FindObjectsOfType<CelestialBody> ();
        Time.fixedDeltaTime = GlobalVars.physicsTimeStep;
        Debug.Log ("Setting fixedDeltaTime to: " + GlobalVars.physicsTimeStep);
    }

    private void FixedUpdate() {
        Time.timeScale = GlobalVars.timeScale;
        if (GlobalVars.simulateCelestialBodies) {
            UpdateCelestialBodyPhysics();
        }
        if (GlobalVars.simulateVirtualBodies) {
            UpdateVirtualBodyPhysics();
        }
    }

    private void UpdateVirtualBodyPhysics() {
        vBodies = FindObjectsOfType<VirtualBody> (); // TODO Optimize, must be better way then to check all the time.
        for (int i = 0; i < vBodies.Length; i++) {
            Vector3 acceleration = CalculateAcceleration (vBodies[i].transform.position, vBodies[i]);
            vBodies[i].UpdateVelocity (acceleration, GlobalVars.physicsTimeStep);
        }

        for (int i = 0; i < vBodies.Length; i++) {
            vBodies[i].UpdatePosition (GlobalVars.physicsTimeStep);
        }
    }

    private void UpdateCelestialBodyPhysics() {
        bodies = FindObjectsOfType<CelestialBody> (); // TODO Optimize, must be better way then to check all the time.
        for (int i = 0; i < bodies.Length; i++) {
            Vector3 acceleration = CalculateAcceleration (bodies[i].transform.position, bodies[i]);
            bodies[i].UpdateVelocity (acceleration, GlobalVars.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].UpdatePosition (GlobalVars.physicsTimeStep);
        }
    }

    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.transform.position - point).sqrMagnitude;
                if ((sqrDst) < (body.radius + ignoreBody.radius)) {
                    return new Vector3(0,0,0); //TODO Should probs merge or explode here
                }
                Vector3 forceDir = (body.transform.position - point).normalized;
                acceleration += forceDir * GlobalVars.gravitationalConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }
    // TODO Too much code is reused, should be able to be merged right?
    public static Vector3 CalculateAcceleration (Vector3 point, VirtualBody ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.vBodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.transform.position - point).sqrMagnitude;
                if ((sqrDst) < (body.radius + ignoreBody.radius)) {
                    return new Vector3(0,0,0); //TODO Should probs merge or explode here
                }
                Vector3 forceDir = (body.transform.position - point).normalized;
                acceleration += forceDir * GlobalVars.gravitationalConstant * body.mass / sqrDst;
            }
        }
        return acceleration;
    }

    public static CelestialBody[] Bodies {
        get {
            return Instance.bodies;
        }
    }

    static Simulation Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<Simulation> ();
            }
            return instance;
        }
    }
}
