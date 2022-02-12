using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlanetTest : MonoBehaviour
{
    public GameObject basic;
    public ColourSettings colourSettings;
    public ShapeSettings shapeSettings;

    public void PlacePlanet()
    {
        // Create basic
        GameObject newPlanet = Instantiate(basic);
        Transform model = newPlanet.transform.GetChild(0);
        // Add celestial body script
        model.gameObject.AddComponent<Planet>();
        Planet planet = model.gameObject.GetComponent<Planet>();
        planet.shapeSettings = shapeSettings;
        planet.colourSettings = colourSettings;
        planet.resolution = 256;

        planet.GeneratePlanet();
    }

    public void PlacePlanet2()
    {
        GameObject newPlanet = Instantiate(basic);
        Planet planet = newPlanet.transform.GetChild(0).gameObject.GetComponent<Planet>();
        planet.GeneratePlanet();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlacePlanet();
        }
    }
}
