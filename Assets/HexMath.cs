using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMath : MonoBehaviour
{
    public static float outerRadius = 1f;
    public static float innerRadius = outerRadius * (float)(Math.Sqrt(3) / 2);

    public static float hexSize = 1.665f;
    
    public Vector3[] corners =
    {
        new Vector3(0, 0, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };
}
