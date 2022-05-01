using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extentions
{
    public static Vector3 Round(this Vector3 vector)
    {
        vector.x = Mathf.Round(vector.x);
        vector.y = Mathf.Round(vector.y);
        vector.z = Mathf.Round(vector.z);
        return vector;
    }
}
