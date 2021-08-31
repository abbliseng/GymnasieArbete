using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class DrawOrbits : MonoBehaviour
{
    public static int steps = 1000;
    public static int width;

    public void Draw() {
        CelestialBody[] simulationVictims = FindObjectsOfType<CelestialBody>();
        foreach (CelestialBody victim in simulationVictims) {
            GameObject objToSpawn = new GameObject("Virtual "+victim.bodyName);
            // objToSpawn.transform.SetParent(this.transform);
            objToSpawn.transform.SetParent(this.transform);
            objToSpawn.AddComponent<Rigidbody>();
            objToSpawn.AddComponent<VirtualBody>();
            
            VirtualBody objToSpawnData = objToSpawn.GetComponent<VirtualBody>();
            objToSpawn.transform.position = victim.transform.position;
            objToSpawn.transform.rotation = victim.transform.rotation;

            // TODO Should probs be a better way for this too. Constructor?
            objToSpawnData.rb = objToSpawn.GetComponent<Rigidbody>();
            objToSpawnData.rb.useGravity = false;
            objToSpawnData.mass = victim.mass;
            objToSpawnData.radius = victim.radius;
            if (victim.velocity == new Vector3(0,0,0)){
                objToSpawnData.initialVelocity = victim.initialVelocity;
            } else {
                objToSpawnData.velocity = victim.velocity;
            }
            objToSpawnData.lr = objToSpawn.AddComponent<LineRenderer>();
        }
    }

    public void DestroyVirtualBodies() {

    }

}
