using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    public Planet planet;
    private void Start()
    {
        Debug.Log(this.name + ": Starting");
        if (planet != null)
        {
            planet.GeneratePlanet();
        }
    }
}
