using System.Collections.Generic;
using UnityEngine;
public class GenericsTest<T>
{
    public static readonly List<T> genericList = new();


    public static void Loop()
    {
        foreach(var t in genericList)
        {
            Debug.Log("LOOP: " + t + " " + t.GetType().ToString());
        }
    }
}