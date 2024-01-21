using UnityEngine;

public class MathUtils
{
    public static Vector2 CircleLengthDir(Vector2 center, float deg, float length)
    {
        float newX = Mathf.Cos(deg * Mathf.Deg2Rad) * length;
        float newY = Mathf.Sin(deg * Mathf.Deg2Rad) * length;
        return new Vector2(newX + center.x, newY + center.y);
    }
}