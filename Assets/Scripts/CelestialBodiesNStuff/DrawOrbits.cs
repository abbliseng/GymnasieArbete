using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class DrawOrbits : MonoBehaviour
{
    public static int steps = 1000;
    public static int width;
    
    private VirtualBody[] bodies;

    public void Draw() {
        CelestialBody[] simulationVictims = FindObjectsOfType<CelestialBody>();
        foreach (CelestialBody victim in simulationVictims) {
            CreateVirtualCopy(victim);
        }
    }


    public void DestroyVirtualBodies() {

    }

    private void FixedUpdate() {
        if (GlobalVars.simulateVirtualBodies) {
            UpdateVirtualBodyPhysics();
        }
    }

    private void UpdateVirtualBodyPhysics() {
        bodies = FindObjectsOfType<VirtualBody> (); // TODO Optimize, must be better way then to check all the time.
        for (int i = 0; i < bodies.Length; i++) {
            Vector3 acceleration = Simulation.CalculateAcceleration(bodies[i].transform.position, bodies[i]);
            bodies[i].UpdateVelocity(acceleration, GlobalVars.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].UpdatePosition(GlobalVars.physicsTimeStep);
        }
    }

    public void CreateVirtualCopy(CelestialBody victim) {
        // Create object and set the name
        GameObject objToSpawn = new GameObject("Virtual "+victim.bodyName);
        // Parent the virtual to its parent
        objToSpawn.transform.SetParent(this.transform);
        objToSpawn.AddComponent<Rigidbody>();
        objToSpawn.AddComponent<VirtualBody>();
        
        VirtualBody objToSpawnData = objToSpawn.GetComponent<VirtualBody>();
        objToSpawn.transform.position = victim.transform.position;
        objToSpawn.transform.rotation = victim.transform.rotation;

        // TODO Should probs be a better way for this too. Constructor?
        objToSpawnData.rb = objToSpawn.GetComponent<Rigidbody>();
        objToSpawnData.rb.useGravity = false;
        // objToSpawnData.mass = victim.mass;
        // objToSpawnData.radius = victim.radius;
        objToSpawnData.parent = victim.gameObject;
        objToSpawnData.parentCel = victim;
        // ???
        objToSpawnData.UpdatePlanetInfo();
        // if (victim.velocity == new Vector3(0,0,0)){
        //     objToSpawnData.initialVelocity = victim.initialVelocity;
        // } else {
        //     objToSpawnData.velocity = victim.velocity;
        // }
        objToSpawnData.lr = objToSpawn.AddComponent<LineRenderer>();
    }

    public void UpdateVirtualCopy(CelestialBody victim, VirtualBody target) {
        target.transform.position = victim.transform.position;
        target.transform.rotation = victim.transform.rotation;
        target.mass = victim.mass;
        target.radius = victim.radius;
        if (victim.velocity == new Vector3(0,0,0)){
            target.initialVelocity = victim.initialVelocity;
        } else {
            target.velocity = victim.velocity;
        }
    }
}
