using UnityEditor;
using UnityEngine;

public class AlignWithVelocity : RigidBodyBehaviour
{
    public float turnSpeed = 0.2f;
    public float headingAngleOffset = 90;

    public void FixedUpdate()
    {
        this.Align();
    }

    public void Align()
    {
        Vector2 heading = rb.velocity.normalized;
        float headingAngle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;

        Quaternion current = Quaternion.Euler(rb.transform.eulerAngles);
        Quaternion target = Quaternion.Euler(new Vector3(0, 0, headingAngle + headingAngleOffset));
        Vector3 eulers = Quaternion.Lerp(current, target, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        rb.transform.eulerAngles = eulers;
    }
}