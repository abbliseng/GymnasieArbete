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
        Time.fixedDeltaTime = GlobalVars.physicsTimeStep;
        Debug.Log ("Setting fixedDeltaTime to: " + GlobalVars.physicsTimeStep);
    }

    void keyHandler() {

    }

    void PlaceObject(GameObject placingObject) {
        GlobalVars.placingObject = placingObject;
        GlobalVars.placing = true;
    }

    private void FixedUpdate() {
        Time.timeScale = GlobalVars.timeScale;
        if (GlobalVars.simulateCelestialBodies) {
            UpdateCelestialBodyPhysics();
        }
        // Handle gameobject placing
        if (GlobalVars.placing) {

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

    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody) {
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
    public static Vector3 CalculateAcceleration (Vector3 point, VirtualBody ignoreBody) {
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
                instance = FindObjectOfType<Simulation>();
            }
            return instance;
        }
    }
}
