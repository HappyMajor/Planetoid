using UnityEngine;
public class PhyUtil
{
    public static Vector2 GetHeading(Rigidbody2D rigidbody)
    {
        Vector2 heading = rigidbody.velocity;

        return heading;
    }
    public static Vector2 GetHeadingDirection(Rigidbody2D rigidbody)
    {
        return GetHeading(rigidbody).normalized;
    }

}