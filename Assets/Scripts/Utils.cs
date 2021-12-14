using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetMousePositionInWorldSpace(float z)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z)); 
    }
    public static Vector3 GetMousePositionInWorldSpace()
    {
        return GetMousePositionInWorldSpace(Camera.main.transform.position.z + 180); //Camera.main.nearClipPlane
    }
}
