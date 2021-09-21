using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class DrawOrbits : MonoBehaviour
{
    public static int steps = 1000;
    public static int width;


    /*
        TODO
        Det är nåt fel när vb skapas utanför editorn. De blir ej rätt.

    */

    // private void Awake() {
    //     CelestialBody[] simulationVictims = FindObjectsOfType<CelestialBody>();
    //     foreach (CelestialBody victim in simulationVictims) {
    //         CreateVirtualCopy(victim);
    //     }
    // }

    public void Draw() {
        CelestialBody[] simulationVictims = FindObjectsOfType<CelestialBody>();
        foreach (CelestialBody victim in simulationVictims) {
            CreateVirtualCopy(victim);
        }
    }


    public void DestroyVirtualBodies() {

    }

    public void CreateVirtualCopy(CelestialBody victim) {
        // Create object and set the name
        GameObject objToSpawn = new GameObject("Virtual "+victim.bodyName);
        // Parent the virtual to its parent
        // objToSpawn.transform.SetParent(victim.transform);
        objToSpawn.transform.SetParent(this.transform);
        objToSpawn.AddComponent<Rigidbody>();
        objToSpawn.AddComponent<VirtualBody>();
        
        VirtualBody objToSpawnData = objToSpawn.GetComponent<VirtualBody>();
        objToSpawn.transform.position = victim.transform.position;
        objToSpawn.transform.rotation = victim.transform.rotation;

        // TODO Should probs be a better way for this too. Constructor?
        objToSpawnData.rb = objToSpawn.GetComponent<Rigidbody>();
        objToSpawnData.rb.useGravity = false;
        // objToSpawnData.rb.mass = victim.mass; // Probs could be prettier
        objToSpawnData.mass = victim.mass;
        objToSpawnData.radius = victim.radius;
        // ???
        if (victim.velocity == new Vector3(0,0,0)){
            objToSpawnData.initialVelocity = victim.initialVelocity;
        } else {
            objToSpawnData.velocity = victim.velocity;
        }
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
