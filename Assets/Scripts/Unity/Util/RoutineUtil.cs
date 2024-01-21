using System;
using System.Collections;
using UnityEngine;

public class RoutineUtil
{
    public static IEnumerator DoLater(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}