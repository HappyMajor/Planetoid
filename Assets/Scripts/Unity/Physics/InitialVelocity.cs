using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : RigidBodyBehaviour
{
    public PositionHandle2D position;
    public float initialSpin;

    public override void Start()
    {
        base.Start();
        rb.velocity = (position.targetPosition - (Vector2)transform.position);
        rb.AddTorque(initialSpin);
    }
}
