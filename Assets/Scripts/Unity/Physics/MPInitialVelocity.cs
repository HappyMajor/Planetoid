using UnityEngine;

public class MPInitialVelocity : NetworkRigidBehaviour
{
    public float spin = 3;
    public override void OnStartServer()
    {
        Debug.Log("On Start Server Add Torque");
        this.GetComponent<Rigidbody2D>().AddTorque(spin);
    }
}