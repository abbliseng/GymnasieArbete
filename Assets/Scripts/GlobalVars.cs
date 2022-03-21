using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars
{
    public const float gravitationalConstant = 0.0001f * 10;
    public const float physicsTimeStep = 0.01f;
    public const bool cheatsEnabled = true;

    public static bool paused = false;
    public static float timeScale = 10f;
    public static int orbitSegments = 100;

    public static bool simulateCelestialBodies = true;
    public static bool simulateVirtualBodies = false;

    public static void Pause()
    {
        paused = true;
    }
    public static void Resume()
    {
        paused = false;
    }

    public static void ChangeSpeed(float inc = 1f)
    {
        timeScale += inc;
        if (timeScale < 1f)
        {
            timeScale = 1f;
        }
    }

}
