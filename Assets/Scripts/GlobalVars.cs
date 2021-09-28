using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars
{
    public const float gravitationalConstant = 0.0001f;
    public const float physicsTimeStep = 0.01f;
    public const bool cheatsEnabled = true;

    public static float timeScale = 10f;
    public static int orbitSegments = 100;

    public static bool simulateCelestialBodies = true;
    public static bool simulateVirtualBodies = true;
    // Place new spheres
    public static bool placing = false;
    public static GameObject placingObject = null;
}
