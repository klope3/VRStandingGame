using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string Vec3String(Vector3 vec)
    {
        return $"({vec.x}, {vec.y}, {vec.z})";
    }

    public static void PrintEnumerable<T>(IEnumerable enumerable, System.Func<T, string> func)
    {
        string str = "";
        foreach (T element in enumerable)
        {
            str += $"{func(element)}, ";
        }
        Debug.Log(str);
    }
}
